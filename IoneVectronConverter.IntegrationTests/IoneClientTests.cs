using System.Xml.Serialization;
using IoneVectronConverter.IoneClient;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IoneVectronConverter.IntegrationTests;

[TestClass]
public class IoneClientTests 
{
    [TestMethod]
    public void GetOrders_ValidDateFilter_ReturnsOrders()
    {
        //Arrange
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddHttpClient<IIoneClient, IoneClient.IoneClient>("ioneClient", client =>
                    {
                        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        //     "Bearer",
                        //     AppSettings.Default.IoneApiToken);
                        // client.DefaultRequestHeaders.Add("Identifier", AppSettings.Default.IoneApiIdentifier);
                        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri("http://localhost:3001");
                    });
                });
            });
        var httpClient = application.CreateClient();
        
        
        //Act
        
        IoneClient.IoneClient client = new(httpClient);

        client.GetOrdersAsync(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));
        //Assert
    }
}