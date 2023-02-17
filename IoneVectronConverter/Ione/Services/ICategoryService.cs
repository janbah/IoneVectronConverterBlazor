using IoneVectronConverter.Ione.Models;
using Order2VPos.Core.IoneApi.ItemCategories;

namespace IoneVectronConverter.Ione.Services;

public interface ICategoryService
{
    bool ExistsMainCategory();
    void Save(ItemCategory itemCategory);
    IEnumerable<Category> GetAll();
}