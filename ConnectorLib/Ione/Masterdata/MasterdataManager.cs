using ConnectorLib.Ione.Client;
using ConnectorLib.Ione.Orders.Models;
using ConnectorLib.Vectron.Masterdata.Models;
using ConnectorLib.Vectron.Masterdata.Services;
using IoneVectronConverter.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ConnectorLib.Ione.Masterdata;

public class MasterdataManager 
{
    private readonly ItemLinkLayerListResponse _itemLinkLayersListResponse; 
    private readonly Item[] _webshopItems;
    private readonly IConfiguration _configuration;
    private readonly IIoneClient _iIoneClient;
    private readonly IPluService _pluService;
    private readonly ITaxService _taxService;
    private readonly ISelWinService _selWinService;
    
    
    private readonly int _branchAdressId;
    private readonly PriceListAssignment[]? _priceListAssignmentList;


    public MasterdataManager(IIoneClient iIoneClient, IConfiguration configuration, IPluService pluService, ITaxService taxService, ISelWinService selWinService)
    {
        _iIoneClient = iIoneClient;
        _configuration = configuration;
        _pluService = pluService;
        _taxService = taxService;
        _selWinService = selWinService;

        _itemLinkLayersListResponse = _iIoneClient.GetLinkLayersAsync();
        _webshopItems = _iIoneClient.GetItemsAsync().Data;

        _branchAdressId = _configuration.GetValue<int>("Vectron:BranchAddressId");
        _priceListAssignmentList =  _configuration.GetSection("Vectron:PriceListAssignmentList").Get<PriceListAssignment[]>();
    }

    public async Task SyncCategories()
    {
        
    }
    
    public async Task SendPlus(bool allItems)
    {
        var vectronMainPlusForWebShop = getMainPlus();
        
        var vectronCondimentPlusForWebShop = getCondimentPlus(vectronMainPlusForWebShop);
        
        var vectronPlusForWebShop = vectronMainPlusForWebShop.Union(vectronCondimentPlusForWebShop).ToArray();

        List<Item> orphandItems = getOrphandItems(_webshopItems);
        List<Item> webshopItemsWithPluNo = getWebshopItemsWithPlu(_webshopItems, vectronPlusForWebShop);
        List<MappingArticle> mappingArticles = getMappingArticles(webshopItemsWithPluNo, ref orphandItems);

        foreach (var vposPlu in vectronPlusForWebShop)
        {
            await sendItemsToWebshop(allItems, mappingArticles, vposPlu, vectronMainPlusForWebShop, vectronCondimentPlusForWebShop);
        }
        
        // Artikelauswahlen zum Webshop übertragen todo: find out what it means

        List<ItemLinkLayer> newBaseItemLinkLayers = getBaseLinkLayer(vectronMainPlusForWebShop, mappingArticles);

        await sendBaseLinkLayers(newBaseItemLinkLayers);
        await deactivatItemsInWebshop(orphandItems);
        // Todo: Write Log   
        //new LogWriter().WriteEntry($"Artikelstammdaten wurden erfolgreich zum Webshop übertragen!", System.Diagnostics.EventLogEntryType.Information, 200);
    }

    private async Task deactivatItemsInWebshop(List<Item> orphanedItems)
    {
        if (_webshopItems != null)
        {
            var items = _webshopItems.Where(x => orphanedItems.Contains(x));
            foreach (var item in items)
            {
                item.ItemWebshopLink = false;
                string jsonText = JsonConvert.SerializeObject(item);
                var response = await _iIoneClient.PostAsync(
                    new Uri("SaveItem", UriKind.Relative),
                    new StringContent(jsonText));
                response.EnsureSuccessStatusCode();
                string responseContentText = await response.Content.ReadAsStringAsync();
                if (!ApiResponse.IsValid(responseContentText, out string errorMessage4))
                {
                    throw new Exception($"Fehler beim Deaktivieren eines Artikels\r\n{errorMessage4}");
                }
            }
        }
    }

    private async Task sendBaseLinkLayers(List<ItemLinkLayer> newBaseItemLinkLayers)
    {
        if (newBaseItemLinkLayers.Count <= 0)
        {
            return;
        }
        
        string linkLayersJsonText = JsonConvert.SerializeObject(newBaseItemLinkLayers);
        var addLinkLayersResponse = await _iIoneClient.PostAsync(
            new Uri("SaveItemLinkLayer", UriKind.Relative),
            new StringContent(linkLayersJsonText));
        addLinkLayersResponse.EnsureSuccessStatusCode();
        string addLinkLayersResponseText = await addLinkLayersResponse.Content.ReadAsStringAsync();

        if (ApiResponse.IsValid(addLinkLayersResponseText, out string errorMessage5))
        {
            var addLinkLayersResponseResult = JsonConvert.DeserializeObject<ItemLinkLayerResponse>(
                addLinkLayersResponseText);
        }
        else
            throw new Exception($"Fehler beim Übertragen der Artikelauswahlen\r\n{errorMessage5}");
    }

    private List<ItemLinkLayer> getBaseLinkLayer(PLU[] vectronMainPlusForWebShop, List<MappingArticle> mappingArticles)
    {
        List<ItemLinkLayer> result = new();
        SelWin[] selWins = _selWinService.GetAll().ToArray();
        
        foreach (var vposPlu in vectronMainPlusForWebShop.Where(x => x.SelectWin?.Length > 0))      
        {
            var selWin = selWins.First(x => x.Number == vposPlu.SelectWin.First());

            List<ItemLinkLayer> itemLinkLayerList = new List<ItemLinkLayer> { };
            foreach (var selPluNo in selWin.PLUNos)
            {
                itemLinkLayerList.Add(
                    new ItemLinkLayer
                    {
                        APIObjectId = $"{selPluNo}",
                        ItemID = mappingArticles.First(x => x.VectronPluNo == selPluNo).IoneRefIdCondiment,
                        BranchAddressId = _branchAdressId
                    });
            }

            ItemLinkLayer linkLayer = new ItemLinkLayer
            {
                APIObjectId = $"{vposPlu.PLUno}",
                BranchAddressId = _branchAdressId,
                ItemID = mappingArticles.First(x => x.VectronPluNo == vposPlu.PLUno).IoneRefIdMain,
                Name = selWin.Name,
                ItemLinkLayerList = itemLinkLayerList.ToArray(),
                SelectionCounter = selWin.SelectCountIone,
                SelectionConstraint = selWin.SelectCompulsion > 0,
                Nullprice = selWin.ZeroPriceAllowed
            };

            result.Add(linkLayer);
        }
        return result;

    }

    private async Task sendItemsToWebshop(bool allItems, List<MappingArticle> mappingArticles, PLU vposPlu, PLU[] vectronMainPlusForWebShop,
        PLU[] vectronCondimentPlusForWebShop)
    {
        MappingArticle mappingArticle = mappingArticles.FirstOrDefault(y => y.VectronPluNo == vposPlu.PLUno);

        bool isMain = vectronMainPlusForWebShop.Contains(vposPlu);
        bool isCondiment = vectronCondimentPlusForWebShop.Contains(vposPlu);
        int count = 1;
        if (isMain && isCondiment)
            count = 2;

        for (int i = 0; i < count; i++)
        {
            int id;
            int itemCategoryId;
            if (i == 0 && isMain)
            {
                id = mappingArticle?.IoneRefIdMain ?? 0;
                itemCategoryId = getCatgoryId();
            }
            else
            {
                id = mappingArticle?.IoneRefIdCondiment ?? 0;
                itemCategoryId = 0;
            }


            List<ItemPrice> itemPrices = new List<ItemPrice>();

            foreach (var priceListAssigment in _priceListAssignmentList)
            {
                Tax[] taxes = _taxService.GetAll().ToArray();
                ItemPrice itemPrice = new ItemPrice
                {
                    BasePriceWithTax = getPrices(vposPlu, priceListAssigment),
                    PriceListId = priceListAssigment.PriceListId,
                    PriceListType = 1,
                    //Todo: control statement
                    // TaxPercentage = taxes.FirstOrDefault(x => x.TaxNo == vposPlu.TaxNo)?.Rate
                    //     .GetDecimalString().Trim() ?? "0"
                };

                itemPrices.Add(itemPrice);
            }

            Item newOrChangedItem = new Item
            {
                APIObjectId = vposPlu.PLUno.ToString(),
                ItemWebshopLink = true,
                BasePriceWithTax = itemPrices.First().BasePriceWithTax,
                Id = id,
                Name = GetName(vposPlu),
                ItemCategoryId = itemCategoryId,
                
                //Todo: Conversion
                // TaxPercentage = _taxService.GetAll().FirstOrDefault(x => x.TaxNo == vposPlu.TaxNo)?.Rate
                //     .GetDecimalString().Trim() ?? "0",
                BranchAddressIdList = new int[] { _branchAdressId },
                ItemPriceList = itemPrices
            };

            var currentItem = _webshopItems.FirstOrDefault(x => x.Id == id);

            if (allItems || IsChanged(currentItem, newOrChangedItem))
            {
                string jsonText = JsonConvert.SerializeObject(newOrChangedItem);
                var response = await _iIoneClient.PostAsync(
                    new Uri("SaveItem", UriKind.Relative),
                    new StringContent(jsonText));
                response.EnsureSuccessStatusCode();
                string responseContentText = await response.Content.ReadAsStringAsync();
                if (ApiResponse.IsValid(responseContentText, out string errorMessage3))
                {
                    var responseResult = JsonConvert.DeserializeObject<ItemResponse>(responseContentText);

                    if (mappingArticle == null)
                    {
                        mappingArticle = new MappingArticle { VectronPluNo = vposPlu.PLUno };
                        mappingArticles.Add(mappingArticle);
                    }

                    if (i == 0 && isMain)
                    {
                        mappingArticle.IoneRefIdMain = responseResult.Data.Id;
                        mappingArticle.ItemCategoryIdMain = responseResult.Data.ItemCategoryId;
                    }
                    else
                        mappingArticle.IoneRefIdCondiment = responseResult.Data.Id;
                }
                else
                    throw new Exception(
                        $"Fehler beim Übertragen eines Artikels");
            }
        }
    }

    private List<MappingArticle> getMappingArticles(List<Item> webshopItemsWithPluNo,  ref List<Item> orphandItems)
    {
        List<MappingArticle> result = new();
        foreach (var apiItemGroup in webshopItemsWithPluNo.GroupBy(x => Convert.ToInt32(x.APIObjectId)))
        {
            
            var currentMappingArticle = result.FirstOrDefault(x => x.VectronPluNo == apiItemGroup.Key);
            if (currentMappingArticle == null)
            {
                currentMappingArticle = new MappingArticle { VectronPluNo = apiItemGroup.Key };
                result.Add(currentMappingArticle);
            }

            Item mainArticle = apiItemGroup.FirstOrDefault(x => x.ItemWebshopLink && x.ItemCategoryId.HasValue);
            if (mainArticle == null)
                //todo: test path
                mainArticle = apiItemGroup.FirstOrDefault(x => x.ItemCategoryId.HasValue);

            var toRemoveItems = apiItemGroup.ToList();
            if (mainArticle != null)
            {
                currentMappingArticle.IoneRefIdMain = mainArticle.Id;
                currentMappingArticle.ItemCategoryIdMain = mainArticle.ItemCategoryId;
                toRemoveItems.Remove(mainArticle);
            }

            Item condimentArticle = apiItemGroup.FirstOrDefault(
                x =>
                    x.ItemWebshopLink &&
                    !x.ItemCategoryId.HasValue &&
                    _itemLinkLayersListResponse.Data.Any(y => y.ItemLinkLayerList.Any(z => z.ItemID == x.Id)));

            if (condimentArticle == null)
                //todo: test path
                condimentArticle = apiItemGroup.FirstOrDefault(
                    x =>
                        !x.ItemCategoryId.HasValue &&
                        _itemLinkLayersListResponse.Data.Any(y => y.ItemLinkLayerList.Any(z => z.ItemID == x.Id)));

            if (condimentArticle != null)
            {
                currentMappingArticle.IoneRefIdCondiment = condimentArticle.Id;
                toRemoveItems.Remove(condimentArticle);
            }

            orphandItems.AddRange(toRemoveItems.Where(x => x.ItemWebshopLink));
        }

        return result;
    }

    private List<Item> getOrphandItems(Item[] webshopItems)
    {
        return webshopItems.Where(i => i.ItemWebshopLink).ToList();
    }

    private List<Item> getWebshopItemsWithPlu(Item[] webshopItems, PLU[] vectronPlusForWebShop)
    {
        var result = new List<Item>();
        foreach (var item in webshopItems)
        {
            //todo: maybe validate
            int apiObjectId = Convert.ToInt32(item.APIObjectId);
            if  (vectronPlusForWebShop.Any(x => x.PLUno == apiObjectId))
            {
                result.Add(item);
            }
        }
        return result;
    }

    private PLU[] getMainPlus()
    {
        var plus = _pluService.GetAll();
        return plus.Where(x => x.IsForWebShop).ToArray();
    }

    private PLU[] getCondimentPlus(PLU[] vposMainPlusForWebShop)
    {
        PLU[] plus = _pluService.GetAll().ToArray();
        SelWin[] selWins = _selWinService.GetAll().ToArray();
        
        return plus
            .Where(
                x => vposMainPlusForWebShop.Any(
                    y => y.SelectWin
                        .Join(selWins, 
                            z => z, 
                            a => a.Number, 
                                
                            (b, c) => c.PLUNos)
                        .Any(selPlus => selPlus.Contains(x.PLUno))))
            .ToArray();
    }

    private bool IsChanged(Item currentItem, Item newOrChangedItem)
            {
                //Todo: implement function
                return true;
            }

    private string getPrices(PLU vposPlu, PriceListAssignment priceListAssignment)
    {
        var prices = vposPlu.Prices;
        var priceData = prices.Where(p => p.Level == priceListAssignment.VectronPriceLevel).FirstOrDefault();
        
        //Todo: Conversion
        var result = "0";//priceData.Price.GetDecimalString() ?? "0";
        return result;

    }

    private string GetName(PLU vposPlu)
    {
        return "test name";
    }

    private int getCatgoryId()
    {
        //todo : get matching category from database
        return 1;
    }
    
}


