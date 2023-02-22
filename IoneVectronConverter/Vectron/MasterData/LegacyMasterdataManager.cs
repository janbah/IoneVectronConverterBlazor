using System.Collections;
using IoneVectronConverter.Common;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Mapper;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Services;
using Newtonsoft.Json;
using Order2VPos.Core.IoneApi;
using Order2VPos.Core.IoneApi.ItemCategories;
using Order2VPos.Core.IoneApi.Items;

namespace IoneVectronConverter.Vectron.MasterData;

public class LegacyMasterdataManager 
{
    
    
    private readonly ItemLinkLayerListResponse itemLinkLayersListResponse; //= await GetAllItemLinkLayersAsync();
    private readonly ItemListResponse itemListResponse; //= await GetAllItemsAsync();
    private readonly IConfiguration _configuration;
    
    private readonly MasterDataResponse vposMasterData;
    private readonly IIoneClient _iIoneClient;
    private readonly int _branchAdressId;


    public LegacyMasterdataManager(IIoneClient iIoneClient, IMasterdataService masterdataService, IConfiguration configuration)
    {
        _iIoneClient = iIoneClient;
        _configuration = configuration;
        _branchAdressId = _configuration.GetValue<int>("Vectron:BranchAddressId");
        vposMasterData = masterdataService.GetMasterdataResponse();
        itemLinkLayersListResponse = _iIoneClient.GetLinkLayersAsync();
        itemListResponse = _iIoneClient.GetItemsAsync();
    }

    public async Task SendPlus(bool allItems)
        {
            // Artikel für Webshop aus Kasse ermitteln

            var vposMainPlusForWebShop = vposMasterData.PLUs.Where(x => x.IsForWebShop).ToArray();
            
            var vposCondimentPlusForWebShop = vposMasterData.PLUs
                .Where(
                    x => vposMainPlusForWebShop.Any(
                        y => y.SelectWin
                            .Join(vposMasterData.SelWins, 
                                z => z, 
                                a => a.Number, 
                                
                                (b, c) => c.PLUNos)
                            .Any(selPlus => selPlus.Contains(x.PLUno))))
                .ToArray();
            
            var vposPlusForWebShop = vposMainPlusForWebShop.Union(vposCondimentPlusForWebShop).ToArray();

            List<MappingArticle> mappingArticles = new List<MappingArticle>();
            List<Item> orphandItems = new List<Item>();
            
            if (itemListResponse.Data != null)
            {
                // Liste mit Artikeln füllen und zu deaktivierende Artikel für Webshop ermitteln

                List<Item> apiItemsWithPluNo = new List<Item>();
                
                foreach (var currentApiItem in itemListResponse.Data)
                {
                    var apiObjectIdIsDigit = int.TryParse(currentApiItem.APIObjectId, out int vectronPluNo);
                    var apiObjectIdIsVectronPluNo = vposPlusForWebShop.Any(x => x.PLUno == vectronPluNo);
                    
                    if (apiObjectIdIsDigit && apiObjectIdIsVectronPluNo)
                    {
                        apiItemsWithPluNo.Add(currentApiItem);
                    }
                    else if (currentApiItem.ItemWebshopLink)
                    {
                        orphandItems.Add(currentApiItem);
                    }
                }

                foreach (var apiItemGroup in apiItemsWithPluNo.GroupBy(x => Convert.ToInt32(x.APIObjectId)))
                {
                    var currentMappingArticle = mappingArticles.FirstOrDefault(x => x.VectronPluNo == apiItemGroup.Key);
                    if (currentMappingArticle == null)
                    {
                        currentMappingArticle = new MappingArticle { VectronPluNo = apiItemGroup.Key };
                        mappingArticles.Add(currentMappingArticle);
                    }

                    Item mainArticle = apiItemGroup.FirstOrDefault(x => x.ItemWebshopLink && x.ItemCategoryId.HasValue);
                    if (mainArticle == null)
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
                            itemLinkLayersListResponse.Data.Any(y => y.ItemLinkLayerList.Any(z => z.ItemID == x.Id)));
                    
                    if (condimentArticle == null)
                        condimentArticle = apiItemGroup.FirstOrDefault(
                            x => 
                                !x.ItemCategoryId.HasValue &&
                                itemLinkLayersListResponse.Data.Any(y => y.ItemLinkLayerList.Any(z => z.ItemID == x.Id)));

                    if (condimentArticle != null)
                    {
                        currentMappingArticle.IoneRefIdCondiment = condimentArticle.Id;
                        toRemoveItems.Remove(condimentArticle);
                    }

                    orphandItems.AddRange(toRemoveItems.Where(x => x.ItemWebshopLink));
                }
            }

            // Artikel zum Webshop übertragen

            foreach (var vposPlu in vposPlusForWebShop)
            {
                MappingArticle mappingArticle = mappingArticles.FirstOrDefault(y => y.VectronPluNo == vposPlu.PLUno);
                
                bool isMain = vposMainPlusForWebShop.Contains(vposPlu);
                bool isCondiment = vposCondimentPlusForWebShop.Contains(vposPlu);
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

                    var priceListAssignmentList = _configuration.GetSection("Vectron:PriceListAssignmentList").Get<PriceListAssignment[]>();
                    
                    foreach (var priceListAssigment in priceListAssignmentList)
                    {
                        ItemPrice itemPrice = new ItemPrice
                        {
                            BasePriceWithTax =  getPrices(vposPlu, priceListAssigment),
                            PriceListId = priceListAssigment.PriceListId,
                            PriceListType = 1,
                            //Todo: control statement
                            TaxPercentage = vposMasterData.Taxes.FirstOrDefault(x => x.TaxNo == vposPlu.TaxNo)?.Rate.GetDecimalString().Trim() ?? "0"
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
                        TaxPercentage = vposMasterData.Taxes.FirstOrDefault(x => x.TaxNo == vposPlu.TaxNo)?.Rate.GetDecimalString().Trim() ?? "0",
                        BranchAddressIdList = new int[] { _branchAdressId },
                        ItemPriceList = itemPrices
                    };

                    var currentItem = itemListResponse.Data?.FirstOrDefault(x => x.Id == id);
                    
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

            // Artikelauswahlen zum Webshop übertragen

            List<ItemLinkLayer> newBaseItemLinkLayers = new List<ItemLinkLayer>();
            foreach (var vposPlu in vposMainPlusForWebShop.Where(x => x.SelectWin?.Length > 0))
            {
                var selWin = vposMasterData.SelWins.First(x => x.Number == vposPlu.SelectWin.First());

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

                newBaseItemLinkLayers.Add(linkLayer);
            }

            if (newBaseItemLinkLayers.Count > 0)
            {
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

            // Artikel im Webshop deaktivieren

            if (itemListResponse.Data != null)
            {
                var items = itemListResponse.Data.Where(x => orphandItems.Contains(x));
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
            // Todo: Write Log   
            //new LogWriter().WriteEntry($"Artikelstammdaten wurden erfolgreich zum Webshop übertragen!", System.Diagnostics.EventLogEntryType.Information, 200);
        }

            private bool IsChanged(Item currentItem, Item newOrChangedItem)
            {
                //Todo: implement function
                return true;
            }

            private int getPriceListId()
            {
                throw new NotImplementedException();
            }

            private string getPrices(PLU vposPlu, PriceListAssignment priceListAssignment)
            {
                var prices = vposPlu.Prices;
                var priceData = prices.Where(p => p.Level == priceListAssignment.VectronPriceLevel).FirstOrDefault();
                var result = priceData.Price.GetDecimalString() ?? "0";
                return result;

            }

            private string GetName(PLU vposPlu)
            {
                return "test name";
            }

            private int getCatgoryId()
            {
                return 1;
            }
    
}

// public class AppSettings
// {
//     public class Default
//     {
//         public static int BranchAddressId { get; set; }
//         public static int AttributeNoForWebShop { get; set; }
//         public static IEnumerable PriceListAssignmentList { get; set; }
//     }
// }

