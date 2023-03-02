namespace IoneVectronConverter.Ione.Categories;

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

    public Category ToCategory(ItemCategory itemCategory)
    {
        Category category = new()
        {
            Name = itemCategory.Name,
            VectronNo = Convert.ToInt32(itemCategory.APIObjectId),
            IoneRefId = itemCategory.Id,
            IsMain = itemCategory.IsMain,
            IsSent = itemCategory.IsSent
        };
        return category;
    }
}