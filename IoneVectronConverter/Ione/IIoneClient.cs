using IoneVectronConverter.Ione.Orders.Models;
using Order2VPos.Core.IoneApi.ItemCategories;

namespace IoneVectronConverter.Ione;

public interface IIoneClient
{
    Task<OrderListResponse> GetOrdersAsync(DateTime from, DateTime to);
    Task<ItemCategoryListResponse> GetCategoriesAsync();
    int SaveItemCategory(ItemCategory category);
    Task<int> GetMainCategoryId();
}