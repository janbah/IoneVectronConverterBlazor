using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Categories;
using IoneVectronConverter.Ione.Mapper;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Services;
using IoneVectronConverterUnitTests.Mocks;
using Microsoft.Extensions.Configuration;
using Moq;

namespace IoneVectronConverterTests;

public class CategoryManagerTests
{
    private IConfiguration _configuration;

    public CategoryManagerTests()
    {
        TestDatabase testDatabase = new();
        _configuration = testDatabase.GetConfiguration();
        testDatabase.ResetDatabase();
    }
    
    
    [Fact]
    public void SynchronizeArticlesFromDatabaseToIoneClient_NoMainCategory_MainCategoryIsCreated()
    {
        //Arrange
        var ioneClientMock = new Mock<IIoneClient>();

        CategoryRepository categoryRepository = new(_configuration);
        ICategoryService categoryService = new CategoryService(categoryRepository);
        CategoryMapper categoryMapper = new();
        
        IoneCategoryManager categoryManager = new IoneCategoryManager(ioneClientMock.Object, categoryService,
        _configuration, categoryMapper); 
        
        //Act
        categoryManager.SynchronizeArticlesFromDatabaseToIoneClient();
        var result = categoryService.GetAll();
        
        //Assert
        Assert.True(result.Any());
        Assert.True(result.First().IsMain);
        Assert.True(result.First().Name == "Main [#10]");

    }
    
    [Fact]
    public void SynchronizeArticlesFromDatabaseToIoneClient_MainCategory_NoMainCategoryIsCreated()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock();

        CategoryRepository categoryRepository = new(_configuration);
        ICategoryService categoryService = new CategoryService(categoryRepository);
        CategoryMapper categoryMapper = new();
        
        IoneCategoryManager categoryManager = new IoneCategoryManager(ioneClientMock.Object, categoryService,
        _configuration, categoryMapper);

        Category category = new()
        {
            Name = "test",
            IsMain = true,
            IsSent = false,
            VectronNo = 425,
            IoneRefId = 123
        };

        categoryRepository.Insert(category);
            
        //Act

        categoryManager.SynchronizeArticlesFromDatabaseToIoneClient();
        var result = categoryService.GetAll();
        
        //Assert
        Assert.True(result.Count()==1);
        Assert.True(result.First().IsMain);
        Assert.True(result.First().Name == "test");

    }
    [Fact]
    public void SynchronizeArticlesFromDatabaseToIoneClient_OneCategoryNotSentInDb_OneCategoryPlusMainIsSent()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock().MockSaveCategoryPostAsync();

        CategoryRepository categoryRepository = new(_configuration);
        ICategoryService categoryService = new CategoryService(categoryRepository);
        CategoryMapper categoryMapper = new();
        
        IoneCategoryManager categoryManager = new IoneCategoryManager(ioneClientMock.Object, categoryService,
        _configuration, categoryMapper);

        Category category = new()
        {
            Name = "test",
            IsMain = false,
            IsSent = false,
            VectronNo = 425,
            IoneRefId = 123
        };

        categoryRepository.Insert(category);
            
        //Act

        categoryManager.SynchronizeArticlesFromDatabaseToIoneClient();
        var result = ioneClientMock.SentCategories;
        
        //Assert
        Assert.True(result.Count()==2);
        Assert.True(!result[1].IsMain);
        Assert.True(result[1].Name == "test");
    }
    
    [Fact]
    public void SynchronizeArticlesFromDatabaseToIoneClient_OneCategoryAlreadySentInDb_JustMainIsSent()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock().MockSaveCategoryPostAsync();

        CategoryRepository categoryRepository = new(_configuration);
        ICategoryService categoryService = new CategoryService(categoryRepository);
        CategoryMapper categoryMapper = new();
        
        IoneCategoryManager categoryManager = new IoneCategoryManager(ioneClientMock.Object, categoryService,
        _configuration, categoryMapper);

        Category category = new()
        {
            Name = "test",
            IsMain = false,
            IsSent = true,
            VectronNo = 425,
            IoneRefId = 123
        };

        categoryRepository.Insert(category);
            
        //Act

        categoryManager.SynchronizeArticlesFromDatabaseToIoneClient();
        var result = ioneClientMock.SentCategories;
        
        //Assert
        Assert.True(result.Count()==1);
        Assert.True(result[0].IsMain);
        Assert.True(result[0].Name == "Main [#10]");
    }
}