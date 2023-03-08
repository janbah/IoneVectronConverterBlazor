using System.Net.Http.Json;
using ConnectorLib.Ione.Categories;
using ConnectorLib.Ione.Orders.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ConnectorLib.Ione.Client
{
    public class IoneClient : IIoneClient
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _iConfiguration;

        DateTime allFromDate = new DateTime(1970, 1, 1);
        DateTime allToDate = DateTime.Now.AddYears(1);

        public IoneClient(HttpClient httpClient, IConfiguration iConfiguration)
        {
            _httpClient = httpClient;
            _iConfiguration = iConfiguration;
        }


        public async Task<OrderListResponse> GetOrdersAsync(DateTime from, DateTime to)
        {
            var client = _httpClient;

            HttpResponseMessage responseMessage = await client.PostAsync("orders",
                new StringContent("CreatedDateFrom=01.01.2023&CreatedDateTo=23.01.2023"));

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

            var mainCategory =
                categories.FirstOrDefault(x => x.Name == mainCategoryName && x.LevelId == 1 && x.APIObjectId == "-1");

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

    }
}