using IoneVectronConverter.Vectron.Client;
using IoneVectronConverter.Vectron.Mapper;

namespace IoneVectronConverterTests;

public class VectronClientTests
{
    [Fact]
    public void FunctionToTest_Prerequisites_Result()
    {
        //Arrange
        ReceiptMapper mapper = new();
        IVectronClient client = new VectronClient(mapper);

        //Act
        var result = client.GetMasterData();

        //Assert
        Assert.True(result.PLUs.Any());
    }
}