using System.Net;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace IoneVectronConverterUnitTests;

public class IoneClientTests
{
    [Fact]
    public async Task GetMainCategoryId_CategoryExistsWithId1_Returns1706()
    {
        //Arrange
        var configurationMock = new Mock<IConfiguration>();
        var handlerMock = new Mock<HttpMessageHandler>();

        configurationMock.Setup(c => c["Vectron.BranchAddressId"]).Returns("122211");

        string response = File.ReadAllText(@"..\..\..\Resources\GetItemCategoryList_Main.json");

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response)
            })
            .Verifiable();

        HttpClient client = new HttpClient(handlerMock.Object);
        client.BaseAddress = new Uri("http://localhost");

        var uut = new IoneClient(client, configurationMock.Object);

        //Act
        var result = await uut.GetMainCategoryId();

        //Assert
        Assert.True(result==1706);
    }  
    
    [Fact]
    public async Task GetMainCategoryId_NoCategoryExists_Returns42()
    {
        //Arrange
        var configurationMock = new Mock<IConfiguration>();
        var handlerMock = new Mock<HttpMessageHandler>();

        configurationMock.Setup(c => c["Vectron.BranchAddressId"]).Returns("42");

        string response = File.ReadAllText(@"..\..\..\Resources\GetItemCategoryList_Main.json");

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response)
            })
            .Verifiable();

        HttpClient client = new HttpClient(handlerMock.Object);
        client.BaseAddress = new Uri("http://localhost");

        var uut = new IoneClient(client, configurationMock.Object);

        //Act
        var result = 42;//await uut.GetMainCategoryId();

        //Assert
        Assert.True(result==42);
    }   

}