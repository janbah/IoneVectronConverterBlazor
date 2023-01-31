using System.Net;
using System.Xml.Serialization;
using IoneVectronConverter.Ione;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IoneVectrinConverterTests;

public class IoneClientTests
{
    [Fact]
    public async Task IoneClientShouldWork()
    {
        WebApplicationFactory<Program> appFactory = new();

        //IHttpClientFactory factory = (IHttpClientFactory)appFactory.CreateClient();

        HttpClient client = appFactory.CreateClient();

        client.BaseAddress = new Uri("http://localhost:3001");
        
        IIoneClient ioneClient = new IoneClient(client);

        var result = await ioneClient.GetOrdersAsync(new DateTime(2022, 01, 01), new DateTime(2022, 12, 31));
        
        //Assert.True(result.StatusCode == 200 );
    }
}