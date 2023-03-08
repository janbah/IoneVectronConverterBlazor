using ConnectorLib.Ione.Client;

namespace ConnectorLib.Ione.Orders.Models
{
    public class ItemResponse : ApiResponse
    {
        public Item Data { get; set; }
    }
}
