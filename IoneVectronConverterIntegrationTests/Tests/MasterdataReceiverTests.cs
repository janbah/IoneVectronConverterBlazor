using Dapper;
using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Vectron.Client;
using IoneVectronConverter.Vectron.MasterData;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Moq;

namespace IoneVectronConverterTests;

public class MasterdataReceiverTests
{
    [Fact]
    public void ReceiveAndStoreMasterdata_OnePlu_OnePluWithPricesAndSelWinsIsStored()
    {
        cleanTable("price_data");
        cleanTable("select_win");
        cleanTable("plu");
        
        //Arrange
        var vectronClientMock = new Mock<IVectronClient>();
        var generator = new MasterdataGenerator();
        var masterDataResponse = generator.createTestData();
        var configuration = getConfiguration();
        
        var pluRepository = new PluRepository(configuration);
        var taxRepository = new TaxRepository(configuration);
        var selWinRepository = new SelWinRepository(configuration);


        vectronClientMock.Setup(v => v.GetMasterData()).Returns(masterDataResponse);
        
        IPluService pluService = new PluService(pluRepository);
        IDepartmentService departmentService = new DepartmentService();
        ITaxService taxService = new TaxService(taxRepository);
        ISelWinService selWinService = new SelWinService(selWinRepository);

        MasterdataReceiver sut = new(vectronClientMock.Object, pluService,taxService, selWinService, departmentService);
        
        //Act
        sut.ReceiveAndStoreMasterdata();
        var result = pluService.GetAll();

        //Assert
        Assert.True(result.Count()==1);
        Assert.True(result.First().PLUno==1);
        Assert.True(result.First().Name1=="name1");
        Assert.True(result.First().Prices.Length == 1);
        Assert.True(result.First().SelectWin.Length == 3);

        cleanTable("price_data");
        cleanTable("select_win");
        cleanTable("plu");
    }
    
    [Fact]
    public void ReceiveAndStoreMasterdata_TaxlistCountIs2_TwoRowsAreStored()
    {
        cleanTable("tax");

        //Arrange
        var vectronClientMock = new Mock<IVectronClient>();
        var generator = new MasterdataGenerator();
        var masterDataResponse = generator.createTestData();
        var configuration = getConfiguration();
        
        var pluRepository = new PluRepository(configuration);
        var taxRepository = new TaxRepository(configuration);
        var selWinRepository = new SelWinRepository(configuration);


        vectronClientMock.Setup(v => v.GetMasterData()).Returns(masterDataResponse);
        
        IPluService pluService = new PluService(pluRepository);
        IDepartmentService departmentService = new DepartmentService();
        ITaxService taxService = new TaxService(taxRepository);
        ISelWinService selWinService = new SelWinService(selWinRepository);

        MasterdataReceiver sut = new(vectronClientMock.Object, pluService,taxService, selWinService, departmentService);
        
        //Act
        sut.ReceiveAndStoreMasterdata();
        var result = taxService.GetAll();

        //Assert
        Assert.True(result.Count() == 2);

        cleanTable("tax");
    }    
    
    [Fact]
    public void ReceiveAndStoreMasterdata_SelWinCountIs1_OneRowIsStored()
    {
        cleanTable("sel_win_plu_name");
        cleanTable("sel_win");

        //Arrange
        var vectronClientMock = new Mock<IVectronClient>();
        var generator = new MasterdataGenerator();
        var masterDataResponse = generator.createTestData();
        var configuration = getConfiguration();
        
        var pluRepository = new PluRepository(configuration);
        var taxRepository = new TaxRepository(configuration);
        var selWinRepository = new SelWinRepository(configuration);

        vectronClientMock.Setup(v => v.GetMasterData()).Returns(masterDataResponse);
        
        IPluService pluService = new PluService(pluRepository);
        IDepartmentService departmentService = new DepartmentService();
        ITaxService taxService = new TaxService(taxRepository);
        ISelWinService selWinService = new SelWinService(selWinRepository);

        MasterdataReceiver sut = new(vectronClientMock.Object, pluService,taxService, selWinService, departmentService);
        
        //Act
        sut.ReceiveAndStoreMasterdata();
        var result = selWinService.GetAll();
        
        //Assert
        Assert.True(result.Count() == 1);
        Assert.True(result.First().Name == "testSelWin1");
        Assert.True(result.First().SelectCount == 1);
        Assert.True(result.First().PLUs.Length == 3);

        cleanTable("sel_win_plu_name");
        cleanTable("sel_win");
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
}