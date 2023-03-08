using ConnectorLib.Ione.Client;

namespace ConnectorLib.Ione.Orders.Models
{
    public class OrderListResponse : ApiResponse
    {
        public OrderListData[] Data { get; set; }
    }
}
