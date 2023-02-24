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
    public void FunctionToTest_Prerequisites_Result()
    {
        //Arrange
        var vectronClientMock = new Mock<IVectronClient>();
        var generator = new MasterdataGenerator();
        var masterDataResponse = generator.createTestData();
        var configuration = getConfiguration();
        
        var pluRepository = new PluRepository(configuration);

        vectronClientMock.Setup(v => v.GetMasterData()).Returns(masterDataResponse);
        
        IPluService pluService = new PluService(pluRepository);
        IDepartmentService departmentService = new DepartmentService();
        ITaxService taxService = new TaxService();
        ISelWinService selWinService = new SelWinService();

        MasterdataReceiver sut = new(vectronClientMock.Object, pluService,taxService, selWinService, departmentService);
        
        //Act
        sut.ReceiveAndStoreMasterdata();
        var result = pluService.GetAll();

        //Assert
        Assert.True(result.Any());

        cleanTable("price_data");
        cleanTable("select_win");
        cleanTable("plu");
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