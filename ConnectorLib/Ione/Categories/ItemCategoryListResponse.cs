using ConnectorLib.Ione.Client;

namespace ConnectorLib.Ione.Categories
{
    public class ItemCategoryListResponse : ApiResponse
    {
        public ItemCategory[] Data { get; set; }
    }
}
