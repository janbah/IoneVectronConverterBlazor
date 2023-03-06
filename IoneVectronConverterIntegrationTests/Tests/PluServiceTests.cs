using Dapper;
using IoneVectronConverter.Common.Masterdata.Repositories;
using IoneVectronConverter.Common.Masterdata.Services;
using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Vectron.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace IoneVectronConverterTests;

[Collection("database")]
public class PluServiceTests : IDisposable
{
    private IConfiguration configuration;
    
    public PluServiceTests()
    {
        TestDatabase testDatabase = new TestDatabase();
        configuration = testDatabase.GetConfiguration();
        testDatabase.ResetDatabase();
    }
    
    [Fact]
    public void StorePluIfNew_ThreePlusInList_DatatableCountIs3()
    {
        //Arrange
  
        var pluRepository = new PluRepository(configuration);
        IPluService pluService = new PluService(pluRepository);
        
        PLU plu1 = createTestPlus()[0];
        PLU plu2 = createTestPlus()[1];
        PLU plu3 = createTestPlus()[2];
        
        var plus1 = new[] { plu1, plu2,plu3 };

        //Act
        pluService.StorePlus(plus1);

        //Assert
        var result = pluService.GetAll();
        Assert.True(result.Count()==3);

    }
    
    [Fact]
    public void StorePluIfNew_FirstListContains3SecondListContains1_DatatableCountIs1()
    {
        //Arrange
        
        var configuration = getConfiguration();
        var pluRepository = new PluRepository(configuration);
        IPluService pluService = new PluService(pluRepository);
        
        PLU plu1 = createTestPlus()[0];
        PLU plu2 = createTestPlus()[1];
        PLU plu3 = createTestPlus()[2];
        
        var plus1 = new[] { plu1, plu2,plu3 };
        var plus2 = new[] { plu1 };

        //Act
        pluService.StorePlus(plus1);
        pluService.StorePlus(plus2);

        //Assert
        var result = pluService.GetAll();
        Assert.True(result.Count()== 1);

    }
    
    

    private PLU[] createTestPlus()
    {
        
        PLU plu1 = new()
        {
            PLUno = 1,
            Name1 = "name1",
            Name2 = "name2",
            Name3 = "name3",
            Attributes = "",
            Prices = new[]
            {
                new PriceData()
                {
                    Price = 20, Level = 1
                }
            },
            DepartmentNo = 1,
            SaleAllowed = true,
            SelectWin = new[] { 1, 2, 3 },
            TaxNo = 1,
            MainGroupA = 0,
            MainGroupB = 1,
            IsForWebShop = true
        };
        
        PLU pluWithSameNo = new()
        {
            PLUno = 1,
            Name1 = "name4",
            Name2 = "name5",
            Name3 = "name6",
            Attributes = "klkkl",
            Prices = new[]
            {
                new PriceData()
                {
                    Price = 7, Level = 2
                }
            },
            DepartmentNo = 2,
            SaleAllowed = true,
            SelectWin = new[] { 4, 5, 6 },
            TaxNo = 2,
            MainGroupA = 0,
            MainGroupB = 2,
            IsForWebShop = true
        };
        
        PLU pluWithDifferentNo = new()
        {
            PLUno = 2,
            Name1 = "name1",
            Name2 = "name2",
            Name3 = "name3",
            Attributes = "",
            Prices = new[]
            {
                new PriceData()
                {
                    Price = 20, Level = 1
                }
            },
            DepartmentNo = 1,
            SaleAllowed = true,
            SelectWin = new[] { 1, 2, 3 },
            TaxNo = 1,
            MainGroupA = 0,
            MainGroupB = 1,
            IsForWebShop = true
        };

         return new[] { plu1, pluWithSameNo, pluWithDifferentNo };
        
    }


    private void cleanTable(string tableName)
    {
        var configuration = getConfiguration();
        var connectionString = configuration.GetConnectionString("Default");

        using (var con = new SqliteConnection(connectionString))
        {
            var sql = string.Format("delete from {0}", tableName);
            con.Execute(sql);
        }
    }

    
    private IConfigurationRoot getConfiguration()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(),"../../../../IoneVectronConverterUnitTests/Resources");
        Console.WriteLine(path);
        return new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public void Dispose()
    {
        TestDatabase testDatabase = new TestDatabase();
        testDatabase.ResetDatabase();
    }
}