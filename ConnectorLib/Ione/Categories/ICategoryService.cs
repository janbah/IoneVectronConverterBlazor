namespace ConnectorLib.Ione.Categories;

public interface ICategoryService
{
    bool ExistsMainCategory();
    void Save(ItemCategory itemCategory);
    long Save(Category category);
    IEnumerable<Category> GetAll();
    void Delete(int id);
    void Update(Category category);
}