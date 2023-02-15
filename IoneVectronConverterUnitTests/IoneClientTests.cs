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
    public async Task GetMainCategoryId_CategoryExistsWithId1_Returns1()
    {
        //Arrange
        var configurationMock = new Mock<IConfiguration>();
        var handlerMock = new Mock<HttpMessageHandler>();

        configurationMock.Setup(c => c["Vectron.BranchAddressId"]).Returns("7");

        Category category = new()
        {
            Id = 1,
            IoneRefId = 1,
            Name = "TestMain",
            VectronNo = 7
        };

        string categoryAsJson = JsonConvert.SerializeObject(category);
        
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
                Content = new StringContent(categoryAsJson)
            })
            .Verifiable();

        HttpClient client = new HttpClient(handlerMock.Object);

        var uut = new IoneClient(client, configurationMock.Object);

        //Act
        var result = await uut.GetMainCategoryId();

        //Assert
        Assert.True(result==1);
    }   
}