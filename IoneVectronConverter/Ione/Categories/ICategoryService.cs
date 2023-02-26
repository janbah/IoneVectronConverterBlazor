using IoneVectronConverter.Ione.Models;
using Order2VPos.Core.IoneApi.ItemCategories;

namespace IoneVectronConverter.Ione.Services;

public interface ICategoryService
{
    bool ExistsMainCategory();
    void Save(ItemCategory itemCategory);
    long Save(Category category);
    IEnumerable<Category> GetAll();
    void Delete(int id);
    void Update(Category category);
}