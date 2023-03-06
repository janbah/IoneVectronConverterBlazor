using IoneVectronConverter.Common.Masterdata.Repositories;
using IoneVectronConverter.Common.Masterdata.Services;
using IoneVectronConverter.Vectron.Client;
using IoneVectronConverter.Vectron.MasterData.Manager;
using Microsoft.Extensions.Configuration;
using Moq;

namespace IoneVectronConverterTests;

[Collection("database")]
public class MasterdataReceiverTests : IDisposable
{
    private IConfigurationRoot _configuration;
    
    public MasterdataReceiverTests()
    {
        TestDatabase testDatabase = new TestDatabase();
        testDatabase.ResetDatabase();
        _configuration = testDatabase.GetConfiguration();
    }

    public void Dispose()
    {
        TestDatabase testDatabase = new TestDatabase();
        testDatabase.ResetDatabase();
    }

    [Fact]
    public void ReceiveAndStoreMasterdata_OnePlu_OnePluWithPricesAndSelWinsIsStored()
    {
        //Arrange
        var vectronClientMock = new Mock<IVectronClient>();
        var generator = new MasterdataGenerator();
        var masterDataResponse = generator.createTestData();
        
        var pluRepository = new PluRepository(_configuration);
        var taxRepository = new TaxRepository(_configuration);
        var selWinRepository = new SelWinRepository(_configuration);

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
        //Assert.True(result.First().Prices.Length == 1);
        Assert.True(result.First().SelectWin.Length == 3);
    }
    
    [Fact]
    public void ReceiveAndStoreMasterdata_TaxlistCountIs2_TwoRowsAreStored()
    {

        //Arrange
        var vectronClientMock = new Mock<IVectronClient>();
        var generator = new MasterdataGenerator();
        var masterDataResponse = generator.createTestData();
        
        var pluRepository = new PluRepository(_configuration);
        var taxRepository = new TaxRepository(_configuration);
        var selWinRepository = new SelWinRepository(_configuration);


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
    }    
    
    [Fact]
    public void ReceiveAndStoreMasterdata_SelWinCountIs1_OneRowIsStored()
    {


        //Arrange
        var vectronClientMock = new Mock<IVectronClient>();
        var generator = new MasterdataGenerator();
        var masterDataResponse = generator.createTestData();
        
        var pluRepository = new PluRepository(_configuration);
        var taxRepository = new TaxRepository(_configuration);
        var selWinRepository = new SelWinRepository(_configuration);

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
    }

}