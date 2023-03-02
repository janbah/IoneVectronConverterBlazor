namespace IoneVectronConverter.Ione.Categories;

public class IoneCategoryManager : IIoneCategoryManager
{

    private readonly IIoneClient _iIoneClient;
    private readonly ICategoryService _categoryService;
    private readonly IConfiguration _iConfiguration;
    private readonly CategoryMapper _categoryMapper;

    public IoneCategoryManager(IIoneClient iIoneClient, ICategoryService categoryService, IConfiguration iConfiguration, CategoryMapper categoryMapper)
    {
        _iIoneClient = iIoneClient;
        _categoryService = categoryService;
        _iConfiguration = iConfiguration;
        _categoryMapper = categoryMapper;
    }


    public void SynchronizeArticlesFromDatabaseToIoneClient()
    {
        //Todo: Write Tests
        
        createMainCategoryIfNotExists();
        
        var categories = getStoredCategories();

        var categoriesNotEvenSend = getStoredCategories().Where(c => c.IsSent is false).ToList();

        sendCategories(categoriesNotEvenSend);
    }
    
    private IEnumerable<Category> getStoredCategories()
    {
        return _categoryService.GetAll();
    }

    private IEnumerable<Category> filterCategories(IEnumerable<Category> categories)
    {
        return categories.Where(c => c.IsSent == false);
    }
    
    private void sendCategories(IEnumerable<Category> categories)
    {
        foreach (var category in categories)
        {
            ItemCategory itemCategory = _categoryMapper.ToItemCategory(category);
            _iIoneClient.SaveCategoryAsync(itemCategory);
            category.IsSent = true;
            _categoryService.Update(category);
        }
    }
    
  
    
    private void storeCategoryToDb(ItemCategory itemCategory)
    {
        //Todo: what is stored?
        
        _categoryService.Save(itemCategory);
    }

    private async Task<int> sendCategoryToIone(ItemCategory itemCategory)
    {
        return await _iIoneClient.SaveCategoryAsync(itemCategory);
    }
    
    private async Task createMainCategoryIfNotExists()
    {
        if (_categoryService.ExistsMainCategory())
        {
            return;
        }

        ItemCategory itemCategory = createMainCategory();
        var mainCategoryId = await sendCategoryToIone(itemCategory);
        itemCategory.Id = mainCategoryId;
        itemCategory.IsSent = true;
        storeCategoryToDb(itemCategory);
    }

    private ItemCategory createMainCategory()
    {
        var branchAdressId = _iConfiguration.GetSection("Vectron").GetValue<int>("BranchAddressId");
        var mainCategoryName = $"Main [#{branchAdressId}]" ;
        
        return new ItemCategory()
        {
            LevelId = 1,
            APIObjectId = "-1",
            Name = mainCategoryName,
            IsMain = true
        };
    }
}

