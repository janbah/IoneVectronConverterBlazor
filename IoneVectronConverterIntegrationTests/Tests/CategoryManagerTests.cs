using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Categories;
using IoneVectronConverter.Ione.Mapper;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Services;
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
}