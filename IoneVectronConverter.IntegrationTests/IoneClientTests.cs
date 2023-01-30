using System.Net;
using System.Xml.Serialization;
using IoneVectronConverter.IoneClient;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Order2VPos.Core.IoneApi.Orders;

namespace IoneVectronConverter.IntegrationTests;

[TestClass]
public class IoneClientTests
{
    private readonly IoneClient.IoneClient _client;

    public IoneClientTests()
    {
        //_client = new(TestClient);
    }
    
    
    [TestMethod]
    public async Task GetOrders_ValidDateFilter_ReturnsOrders()
    {
        //Arrange
        var factory = new WebApplicationFactory<Program>();

        var client = factory.CreateClient();
        client.BaseAddress = new Uri("https://0.0.0.0:3001/");
   
        
        //Act
        
        var response = await client.GetAsync("orders");
        
        
            
        string jsonText = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<OrderListResponse>(jsonText);
        //Task<OrderListResponse> result = _client.GetOrdersAsync(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));
        
        //Assert
        Assert.AreEqual(200,result.StatusCode);
    }
}