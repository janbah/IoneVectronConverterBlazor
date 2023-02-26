using IoneVectronConverter.Ione.Datastoring;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Services;
using Order2VPos.Core.IoneApi.ItemCategories;

namespace IoneVectronConverter.Ione.Categories;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repository;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public bool ExistsMainCategory()
    {
        throw new NotImplementedException();
    }

    public void Save(ItemCategory itemCategory)
    {
        throw new NotImplementedException();
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