using IoneVectronConverter.Ione.Mapper;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;
using IoneVectronConverter.Vectron.MasterData;
using Newtonsoft.Json;
using Order2VPos.Core.IoneApi;
using Order2VPos.Core.IoneApi.ItemCategories;
using Order2VPos.Core.IoneApi.Items;

namespace IoneVectronConverter.Ione
{
    public class IoneClient : IIoneClient
    {   
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;
        private IConfiguration _iConfiguration;
        
        DateTime allFromDate = new DateTime(1970, 1, 1);
        DateTime allToDate = DateTime.Now.AddYears(1);

        // public IoneClient(IHttpClientFactory clientFactory)
        // {
        //     _clientFactory = clientFactory;
        // }
        
        public IoneClient(HttpClient httpClient, IConfiguration iConfiguration)
        {
            _httpClient = httpClient;
            _iConfiguration = iConfiguration;
        }
  

        public async Task<OrderListResponse> GetOrdersAsync(DateTime from, DateTime to)
        {
            var client = _httpClient;
            
            HttpResponseMessage responseMessage = await client.PostAsync("orders",new StringContent("CreatedDateFrom=01.01.2023&CreatedDateTo=23.01.2023"));
            
            string jsonText = await responseMessage.Content.ReadAsStringAsync();
            
            var result = JsonConvert.DeserializeObject<OrderListResponse>(jsonText);
            return result;
        }

        public Task<ItemCategoryListResponse> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public int SaveItemCategory(ItemCategory category)
        {
            throw new NotImplementedException();
        }

    
        public async Task<ItemListResponse> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ItemLinkLayerListResponse> GetAllItemLinkLayersAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<ItemCategoryListResponse> GetAllCategoriesAsync()
        {
            var responseMessage = await _httpClient.PostAsync("GetCategories", new StringContent(""));
            var result = await responseMessage.Content.ReadFromJsonAsync<ItemCategoryListResponse>();
            return result;
        }
        
        public async Task<int> SaveCategoryAsync(ItemCategory category)
        {
            var categoryAsJson = JsonConvert.SerializeObject(category); 
           
            var content = new StringContent(categoryAsJson);
            
            var responseMessage = await _httpClient.PostAsync("SaveItemCategory", content);
            var result = await responseMessage.Content.ReadFromJsonAsync<ItemCategoryResponse>();
            return result.Data.Id;
        }

        public ItemLinkLayerListResponse GetLinkLayersAsync()
        {
            throw new NotImplementedException();
        }

        public ItemListResponse GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PostAsync(Uri uri, StringContent stringContent)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetMainCategoryId()
        {
                    
            string branchAdressId = _iConfiguration["Vectron.BranchAddressId"];
            string mainCategoryName = $"Main [#{branchAdressId}]";
            
            var categoryListResponse = await GetAllCategoriesAsync();
            
            var categories = categoryListResponse.Data;
            
            var mainCategory = categories.FirstOrDefault(x => x.Name == mainCategoryName && x.LevelId == 1 && x.APIObjectId == "-1");

            if (mainCategory is not null)
            {
                return mainCategory.Id;
            }

            ItemCategory defaultCategory = new()
            {
                LevelId = 1,
                APIObjectId = "-1",
                Name = mainCategoryName
            };

            return await sendCategory(defaultCategory);
        }

        private async Task<int> sendCategory(ItemCategory defaultCategory)
        {
            var response = await SaveCategoryAsync(defaultCategory);
            var result = response;
            return result;
        }

        
            //    public Task<ItemCategoryListResponse> GetCategoriesAsync()
     //    {
     //        throw new NotImplementedException();
     //    }
     //
     //    public int SaveItemCategory(ItemCategory category)
     //    {
     //        
     //        throw new NotImplementedException();
     //    }
     //
     //
     //
     //
     //    public async Task SendPlus(bool allItems)
     //    {
     //        ItemCategoryListResponse categoryListResponse = await GetAllCategoriesAsync();
     //        ItemLinkLayerListResponse itemLinkLayersListResponse = await GetAllItemLinkLayersAsync();
     //        ItemListResponse itemListResponse = await GetAllItemsAsync();
     //        
     //        MasterDataResponse vposMasterData = VPosCom.GetMasterData();
     //
     //        if (itemLinkLayersListResponse.StatusCode != 200 && itemLinkLayersListResponse.StatusCode != 0)
     //        {
     //            throw new Exception($"Fehler beim Abruf der IONE Artikelauswahl {itemLinkLayersListResponse.Message}");
     //        }
     //        
     //        if (itemListResponse.StatusCode != 200 && itemListResponse.StatusCode != 0)
     //        {
     //            throw new Exception($"Fehler beim Abruf der IONE Artikel {itemListResponse.Message}");
     //        }
     //        
     //        if (categoryListResponse.StatusCode != 200 && categoryListResponse.StatusCode != 0)
     //        {
     //            throw new Exception($"Fehler beim Abruf der IONE Kategorien {categoryListResponse.Message}");
     //        }
     //        
     //        //CoreDbContext dbContext = CoreDbContext.GetContext();
     //
     // }
     //
     //    private static async Task deactivateArticleInWebshop(ItemListResponse itemListResponse, List<Item> orphandItems)
     //    {
     //        if (itemListResponse.Data != null)
     //        {
     //            var items = itemListResponse.Data.Where(x => orphandItems.Contains(x));
     //            foreach (var item in items)
     //            {
     //                item.ItemWebshopLink = false;
     //                string jsonText = JsonConvert.SerializeObject(item);
     //                var response = await httpClient.PostAsync(
     //                    new Uri("SaveItem", UriKind.Relative),
     //                    new StringContent(jsonText));
     //                response.EnsureSuccessStatusCode();
     //                string responseContentText = await response.Content.ReadAsStringAsync();
     //                if (!ApiResponse.IsValid(responseContentText, out string errorMessage4))
     //                {
     //                    throw new Exception($"Fehler beim Deaktivieren eines Artikels\r\n{errorMessage4}");
     //                }
     //            }
     //        }
     //    }
     //
     //    private static async Task sendArticleSelectionToWebshop(PLU[] vposMainPlusForWebShop, MasterDataResponse vposMasterData,
     //        List<MappingArticle> mappingArticles)
     //    {
     //        List<ItemLinkLayer> newBaseItemLinkLayers = new List<ItemLinkLayer>();
     //        foreach (var vposPlu in vposMainPlusForWebShop.Where(x => x.SelectWin?.Length > 0))
     //        {
     //            var selWin = vposMasterData.SelWins.First(x => x.Number == vposPlu.SelectWin.First());
     //
     //            List<ItemLinkLayer> itemLinkLayerList = new List<ItemLinkLayer> { };
     //            foreach (var selPluNo in selWin.PLUNos)
     //            {
     //                itemLinkLayerList.Add(
     //                    new ItemLinkLayer
     //                    {
     //                        APIObjectId = $"{selPluNo}",
     //                        ItemID = mappingArticles.First(x => x.VectronPluNo == selPluNo).IoneRefIdCondiment,
     //                        BranchAddressId = AppSettings.Default.BranchAddressId
     //                    });
     //            }
     //
     //            ItemLinkLayer linkLayer = new ItemLinkLayer
     //            {
     //                APIObjectId = $"{vposPlu.PLUno}",
     //                BranchAddressId = AppSettings.Default.BranchAddressId,
     //                ItemID = mappingArticles.First(x => x.VectronPluNo == vposPlu.PLUno).IoneRefIdMain,
     //                Name = selWin.Name,
     //                ItemLinkLayerList = itemLinkLayerList.ToArray(),
     //                SelectionCounter = selWin.SelectCountIone,
     //                SelectionConstraint = selWin.SelectCompulsion > 0,
     //                Nullprice = selWin.ZeroPriceAllowed
     //            };
     //
     //            newBaseItemLinkLayers.Add(linkLayer);
     //        }
     //
     //        if (newBaseItemLinkLayers.Count > 0)
     //        {
     //            string linkLayersJsonText = JsonConvert.SerializeObject(newBaseItemLinkLayers);
     //            var addLinkLayersResponse = await httpClient.PostAsync(
     //                new Uri("SaveItemLinkLayer", UriKind.Relative),
     //                new StringContent(linkLayersJsonText));
     //            addLinkLayersResponse.EnsureSuccessStatusCode();
     //            string addLinkLayersResponseText = await addLinkLayersResponse.Content.ReadAsStringAsync();
     //
     //            if (ApiResponse.IsValid(addLinkLayersResponseText, out string errorMessage5))
     //            {
     //                var addLinkLayersResponseResult = JsonConvert.DeserializeObject<ItemLinkLayerResponse>(
     //                    addLinkLayersResponseText);
     //            }
     //            else
     //                throw new Exception($"Fehler beim Übertragen der Artikelauswahlen\r\n{errorMessage5}");
     //        }
     //    }
     //
     //    private static async Task sendArticleToWebshop(bool allItems, PLU[] vposPlusForWebShop, List<MappingArticle> mappingArticles,
     //        PLU[] vposMainPlusForWebShop, PLU[] vposCondimentPlusForWebShop, CoreDbContext dbContext,
     //        MasterDataResponse vposMasterData, ItemListResponse itemListResponse)
     //    {
     //        foreach (var vposPlu in vposPlusForWebShop)
     //        {
     //            MappingArticle mappingArticle = mappingArticles.FirstOrDefault(y => y.VectronPluNo == vposPlu.PLUno);
     //            bool isMain = vposMainPlusForWebShop.Contains(vposPlu);
     //            bool isCondiment = vposCondimentPlusForWebShop.Contains(vposPlu);
     //            int count = 1;
     //            if (isMain && isCondiment)
     //                count = 2;
     //
     //            for (int i = 0; i < count; i++)
     //            {
     //                int id;
     //                int itemCategoryId;
     //                if (i == 0 && isMain)
     //                {
     //                    id = mappingArticle?.IoneRefIdMain ?? 0;
     //                    itemCategoryId =
     //                        dbContext.Categories.FirstOrDefault(x => x.VectronNo == vposPlu.MainGroupB)?.IoneRefId ??
     //                        0;
     //                }
     //                else
     //                {
     //                    id = mappingArticle?.IoneRefIdCondiment ?? 0;
     //                    itemCategoryId = 0;
     //                }
     //
     //
     //                List<ItemPrice> itemPrices = new List<ItemPrice>();
     //
     //                foreach (var priceListAssigment in AppSettings.Default.PriceListAssignmentList)
     //                {
     //                    ItemPrice itemPrice = new ItemPrice
     //                    {
     //                        BasePriceWithTax =
     //                            vposPlu.Prices.FirstOrDefault(x => x.Level == priceListAssigment.VectronPriceLevel)?.Price
     //                                .GetDecimalString() ?? "0",
     //                        PriceListId = priceListAssigment.PriceListId,
     //                        PriceListType = 1,
     //                        TaxPercentage = vposMasterData.Taxes.FirstOrDefault(x => x.TaxNo == vposPlu.TaxNo)?.Rate
     //                            .GetDecimalString().Trim() ?? "0"
     //                    };
     //
     //                    itemPrices.Add(itemPrice);
     //                }
     //
     //                Item newOrChangedItem = new Item
     //                {
     //                    APIObjectId = vposPlu.PLUno.ToString(),
     //                    ItemWebshopLink = true,
     //                    BasePriceWithTax = itemPrices.First().BasePriceWithTax,
     //                    Id = id,
     //                    Name = GetName(vposPlu),
     //                    ItemCategoryId = itemCategoryId,
     //                    TaxPercentage = vposMasterData.Taxes.FirstOrDefault(x => x.TaxNo == vposPlu.TaxNo)?.Rate
     //                        .GetDecimalString().Trim() ?? "0",
     //                    BranchAddressIdList = new int[] { AppSettings.Default.BranchAddressId },
     //                    ItemPriceList = itemPrices
     //                };
     //
     //                var currentItem = itemListResponse.Data?.FirstOrDefault(x => x.Id == id);
     //                if (allItems || IsChanged(currentItem, newOrChangedItem))
     //                {
     //                    string jsonText = JsonConvert.SerializeObject(newOrChangedItem);
     //                    var response = await httpClient.PostAsync(
     //                        new Uri("SaveItem", UriKind.Relative),
     //                        new StringContent(jsonText));
     //                    response.EnsureSuccessStatusCode();
     //                    string responseContentText = await response.Content.ReadAsStringAsync();
     //                    if (ApiResponse.IsValid(responseContentText, out string errorMessage3))
     //                    {
     //                        var responseResult = JsonConvert.DeserializeObject<ItemResponse>(responseContentText);
     //
     //                        if (mappingArticle == null)
     //                        {
     //                            mappingArticle = new MappingArticle { VectronPluNo = vposPlu.PLUno };
     //                            mappingArticles.Add(mappingArticle);
     //                        }
     //
     //                        if (i == 0 && isMain)
     //                        {
     //                            mappingArticle.IoneRefIdMain = responseResult.Data.Id;
     //                            mappingArticle.ItemCategoryIdMain = responseResult.Data.ItemCategoryId;
     //                        }
     //                        else
     //                            mappingArticle.IoneRefIdCondiment = responseResult.Data.Id;
     //                    }
     //                    else
     //                        throw new Exception(
     //                            $"Fehler beim Übertragen eines Artikels [{vposPlu.PLUno}] {GetName(vposPlu)}\r\n{errorMessage3}");
     //                }
     //            }
     //        }
     //    }
     //
     //    private static PLU[] identifyArticlesForWebshop(MasterDataResponse vposMasterData, ItemListResponse itemListResponse,
     //        ItemLinkLayerListResponse itemLinkLayersListResponse, out PLU[] vposCondimentPlusForWebShop,
     //        out PLU[] vposPlusForWebShop, out List<MappingArticle> mappingArticles, out List<Item> orphandItems)
     //    {
     //        var vposMainPlusForWebShop = vposMasterData.PLUs.Where(x => x.IsForWebShop).ToArray();
     //        vposCondimentPlusForWebShop = vposMasterData.PLUs
     //            .Where(
     //                x => vposMainPlusForWebShop.Any(
     //                    y => y.SelectWin
     //                        .Join(vposMasterData.SelWins, z => z, a => a.Number, (b, c) => c.PLUNos)
     //                        .Any(selPlus => selPlus.Contains(x.PLUno))))
     //            .ToArray();
     //        vposPlusForWebShop = vposMainPlusForWebShop.Union(vposCondimentPlusForWebShop).ToArray();
     //
     //        mappingArticles = new List<MappingArticle>();
     //        orphandItems = new List<Item>();
     //
     //        if (itemListResponse.Data != null)
     //        {
     //            // Liste mit Artikeln füllen und zu deaktivierende Artikel für Webshop ermitteln
     //
     //            List<Item> apiItemsWithPluNo = new List<Item>();
     //            foreach (var currentApiItem in itemListResponse.Data)
     //            {
     //                if (int.TryParse(currentApiItem.APIObjectId, out int vectronPluNo) &&
     //                    vposPlusForWebShop.Any(x => x.PLUno == vectronPluNo))
     //                    apiItemsWithPluNo.Add(currentApiItem);
     //                else if (currentApiItem.ItemWebshopLink)
     //                    orphandItems.Add(currentApiItem);
     //            }
     //
     //            
     //            foreach (var apiItemGroup in apiItemsWithPluNo.GroupBy(x => Convert.ToInt32(x.APIObjectId)))
     //            {
     //                var currentMappingArticle = mappingArticles.FirstOrDefault(x => x.VectronPluNo == apiItemGroup.Key);
     //                if (currentMappingArticle == null)
     //                {
     //                    currentMappingArticle = new MappingArticle { VectronPluNo = apiItemGroup.Key };
     //                    mappingArticles.Add(currentMappingArticle);
     //                }
     //
     //                Item mainArticle = apiItemGroup.FirstOrDefault(x => x.ItemWebshopLink && x.ItemCategoryId.HasValue);
     //                if (mainArticle == null)
     //                    mainArticle = apiItemGroup.FirstOrDefault(x => x.ItemCategoryId.HasValue);
     //
     //                var toRemoveItems = apiItemGroup.ToList();
     //                if (mainArticle != null)
     //                {
     //                    currentMappingArticle.IoneRefIdMain = mainArticle.Id;
     //                    currentMappingArticle.ItemCategoryIdMain = mainArticle.ItemCategoryId;
     //                    toRemoveItems.Remove(mainArticle);
     //                }
     //
     //                Item condimentArticle = apiItemGroup.FirstOrDefault(
     //                    x => x.ItemWebshopLink &&
     //                         !x.ItemCategoryId.HasValue &&
     //                         itemLinkLayersListResponse.Data.Any(y => y.ItemLinkLayerList.Any(z => z.ItemID == x.Id)));
     //                
     //                if (condimentArticle == null)
     //                    condimentArticle = apiItemGroup.FirstOrDefault(
     //                        x => !x.ItemCategoryId.HasValue &&
     //                             itemLinkLayersListResponse.Data.Any(y => y.ItemLinkLayerList.Any(z => z.ItemID == x.Id)));
     //
     //                if (condimentArticle != null)
     //                {
     //                    currentMappingArticle.IoneRefIdCondiment = condimentArticle.Id;
     //                    toRemoveItems.Remove(condimentArticle);
     //                }
     //
     //                orphandItems.AddRange(toRemoveItems.Where(x => x.ItemWebshopLink));
     //            }
     //        }
     //
     //        return vposMainPlusForWebShop;
     //    }
     //
     //    private static async Task processCategories(ItemCategoryListResponse categoryListResponse, int mainCategoryIoneRefId,
     //        CoreDbContext dbContext)
     //    {
     //        var currentCategories = categoryListResponse.Data
     //            .Where(
     //                x => x.BranchAddressIdList.Contains(AppSettings.Default.BranchAddressId) &&
     //                     x.ParentId == mainCategoryIoneRefId)
     //            .ToArray();
     //
     //        foreach (var currentCategory in currentCategories)
     //        {
     //            if (int.TryParse(currentCategory.APIObjectId, out int vectronNo))
     //            {
     //                var dbCategory = dbContext.Categories.FirstOrDefault(x => x.VectronNo == vectronNo);
     //                if (dbCategory == null)
     //                {
     //                    dbCategory = new Category
     //                    {
     //                        Name = currentCategory.Name,
     //                        IoneRefId = currentCategory.Id,
     //                        VectronNo = vectronNo
     //                    };
     //                    dbContext.Categories.Add(dbCategory);
     //                }
     //                else
     //                    dbCategory.IoneRefId = currentCategory.Id;
     //            }
     //        }
     //
     //        await dbContext.SaveChangesAsync();
     //
     //        foreach (var dbCategory in dbContext.Categories)
     //        {
     //            ItemCategory itemCategory = currentCategories.FirstOrDefault(x => x.Id == dbCategory.IoneRefId);
     //            if (dbCategory.IoneRefId == 0 ||
     //                !(itemCategory != null &&
     //                  dbCategory.Name == itemCategory.Name &&
     //                  itemCategory.ParentId == mainCategoryIoneRefId))
     //            {
     //                var response = await httpClient.PostAsync(
     //                    new Uri("SaveItemCategory", UriKind.Relative),
     //                    new StringContent(
     //                        JsonConvert.SerializeObject(
     //                            new ItemCategory
     //                            {
     //                                Name = dbCategory.Name?.Trim(),
     //                                APIObjectId = $"{dbCategory.VectronNo}",
     //                                Id = dbCategory.IoneRefId,
     //                                ParentId = mainCategoryIoneRefId
     //                            })));
     //                response.EnsureSuccessStatusCode();
     //                string responseContentText = await response.Content.ReadAsStringAsync();
     //
     //                if (ApiResponse.IsValid(responseContentText, out string errorMessage2))
     //                {
     //                    var responseResult = JsonConvert.DeserializeObject<ItemCategoryResponse>(responseContentText);
     //                    dbCategory.IoneRefId = responseResult.Data.Id;
     //                    dbCategory.Name = responseResult.Data.Name;
     //                }
     //                else
     //                    throw new Exception(
     //                        $"Fehler beim Übertragen der Kategorie [{dbCategory.VectronNo}] {dbCategory.Name}\r\n{errorMessage2}");
     //            }
     //        }
     //
     //        await dbContext.SaveChangesAsync();
     //    }
     //
     //   

    }
}