using IoneVectronConverter.Ione;
using Microsoft.Extensions.Configuration;
using Moq;

namespace IoneVectronConverterUnitTests;

public class IoneClientTests
{
    [Fact]
    public void GetMainCategoryId_CategoryExistsWithId1_Returns1()
    {
        //Arrange
        var configurationMock = new Mock<IConfiguration>();
        var clientMock = new Mock<HttpClient>();
        
        //Todo: implement test
        //clientMock.Setup(c=>c.GetAsync())

        var uut = new IoneClient(clientMock.Object, configurationMock.Object);

        //Act

        //Assert
    }   
}