using ConnectorLib.Ione.Categories;
using ConnectorLib.Ione.Orders.Models;

namespace ConnectorLib.Ione.Client;

public interface IIoneClient
{
    Task<OrderListResponse> GetOrdersAsync(DateTime from, DateTime to);
    Task<ItemCategoryListResponse> GetCategoriesAsync();
    int SaveItemCategory(ItemCategory category);
    Task<int> GetMainCategoryId();

    Task<int> SaveCategoryAsync(ItemCategory category);
    ItemLinkLayerListResponse GetLinkLayersAsync();
    ItemListResponse GetItemsAsync();
    Task<HttpResponseMessage> PostAsync(Uri uri, StringContent stringContent);
}