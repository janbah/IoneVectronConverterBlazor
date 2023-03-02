using IoneVectronConverter.Ione.Categories;
using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione;

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