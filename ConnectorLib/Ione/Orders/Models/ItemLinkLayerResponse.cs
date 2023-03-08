using ConnectorLib.Ione.Client;

namespace ConnectorLib.Ione.Orders.Models
{
    public class ItemLinkLayerResponse : ApiResponse
    {
        public ItemLinkLayer[] Data { get; set; }
    }
}
