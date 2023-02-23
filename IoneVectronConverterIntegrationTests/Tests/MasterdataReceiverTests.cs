using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Vectron.Client;
using IoneVectronConverter.Vectron.MasterData;
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

        vectronClientMock.Setup(v => v.GetMasterData()).Returns(masterDataResponse);

        PluRepository pluRepository = new();
        
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
    }
}