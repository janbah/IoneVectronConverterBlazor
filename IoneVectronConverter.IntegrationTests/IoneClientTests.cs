using System.Xml.Serialization;

namespace IoneVectronConverter.IntegrationTests;

[TestClass]
public class IoneClientTests : IntegrationTest
{
    [TestMethod]
    public void GetOrders_ValidDateFilter_ReturnsOrders()
    {
        //Arrange
        
        
        
        //Act
        
        IoneClient.IoneClient client = new(TestClient);

        client.GetOrdersAsync(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));
        //Assert
    }
}