using System.Text;
using Newtonsoft.Json;
using Order2VPos.Core.IoneApi.Orders;

namespace IoneVectronConverter.IoneClient
{
    public class IoneClient : IIoneClient
    {
        private readonly IHttpClientFactory _clientFactory;
        
        DateTime allFromDate = new DateTime(1970, 1, 1);
        DateTime allToDate = DateTime.Now.AddYears(1);

        public IoneClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
  

        public async Task<OrderListResponse> GetOrdersAsync(DateTime from, DateTime to)
        {
            var client = _clientFactory.CreateClient("ioneClient");

            //string data = $"{{\"CreatedDateFrom\":\"{string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:dd.MM.yyyy HH:mm}", allFromDate)}\",\"CreatedDateTo\":\"{string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:dd.MM.yyyy HH:mm}", allToDate)}\",\"BranchAddressId\": {AppSettings.Default.BranchAddressId}}}";

            // var result = await _httpClient.GetFromJsonAsync<OrderListResponse>("orders");
            HttpResponseMessage responseMessage = await client.PostAsync("orders",new StringContent("CreatedDateFrom=01.01.2023&CreatedDateTo=23.01.2023"));
            
            string jsonText = await responseMessage.Content.ReadAsStringAsync();
            
            var result = JsonConvert.DeserializeObject<OrderListResponse>(jsonText);
            return result;
        }
    }
}