using IoneVectronConverter.Vectron.Client;
using IoneVectronConverter.Vectron.Models;
using IoneVectronConverterTests;
using Microsoft.Extensions.DependencyInjection;

namespace IoneVectronConverterTests;

public class VectronClientTests
{
    
    private readonly CustomWebApplicationFactory<Program> _webApplicationFactory;
    
    public VectronClientTests()
    {
        _webApplicationFactory= new CustomWebApplicationFactory<Program>();
        // resetDataBase();

    }
    
    [Fact]
    public async Task FunctionToTest_Prerequisites_Result()
    {
        //Arrange
        var sut = _webApplicationFactory.Services.GetRequiredService<IVectronClient>();
        Receipt receipt = new()
        {
            Gc = 1,
            Operator = 1,
            Discounts = new List<Discount>(),
            Plus = new List<Plu>(),
            GcText = "gc text",
            MediaNo = 1,
            OperatorCode = 1
        };
        
        //Act
        var result = await sut.SendReceipt(receipt);

        //Assert
        Assert.False(result.IsError);
        Assert.True(result.Message == "success");
    }   
}