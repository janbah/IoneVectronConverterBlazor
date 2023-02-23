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

        //MasterdataService masterdataService = new();

        //MasterdataReceiver masterdataReceiver = new(masterdataService, vectronClientMock.Object);
        //Act

        //Assert
    }
}