using ConnectorLib.Ione.Client;

namespace ConnectorLib.Ione.Orders.Models
{
    public class ItemLinkLayerListResponse : ApiResponse
    {
        public ItemLinkLayer[] Data { get; set; }
    }
}
