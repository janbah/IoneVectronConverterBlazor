using IoneVectronConverter.Ione.Models;
using Order2VPos.Core.IoneApi.ItemCategories;

namespace IoneVectronConverter.Ione.Mapper;

public class CategoryMapper
{
    public ItemCategory ToItemCategory(Category category)
    {
        return new ItemCategory()
        {
            Id = category.Id,
            Name = category.Name,
            
        };
    }
}