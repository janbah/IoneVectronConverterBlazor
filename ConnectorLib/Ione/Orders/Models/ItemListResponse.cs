using ConnectorLib.Ione.Client;

namespace ConnectorLib.Ione.Orders.Models
{
    public class ItemListResponse : ApiResponse
    {
        public Item[] Data { get; set; }
    }
}
