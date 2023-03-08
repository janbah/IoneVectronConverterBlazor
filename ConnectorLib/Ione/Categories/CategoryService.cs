using ConnectorLib.Common.Datastoring;

namespace ConnectorLib.Ione.Categories;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repository;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public bool ExistsMainCategory()
    {
        var result = GetAll().Where(c => c.IsMain).Any();
        return result;
    }

    public void Save(ItemCategory itemCategory)
    {
        CategoryMapper mapper = new();
        Category category = mapper.ToCategory(itemCategory);
        Save(category);
    }

    public long Save(Category category)
    {
        return _repository.Insert(category);
    }

    public IEnumerable<Category> GetAll()
    {
        return _repository.Load();
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public void Update(Category category)
    {
        _repository.Update(category);
    }
}