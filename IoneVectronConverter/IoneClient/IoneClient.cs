using Order2VPos.Core.IoneApi.Orders;

namespace IoneVectronConverter.IoneClient
{
    public class IoneClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        DateTime allFromDate = new DateTime(1970, 1, 1);
        DateTime allToDate = DateTime.Now.AddYears(1);

        public IoneClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OrderListResponse> GetOrdersAsync(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}