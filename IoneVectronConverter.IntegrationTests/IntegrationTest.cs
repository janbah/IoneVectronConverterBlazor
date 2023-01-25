using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IoneVectronConverter.IntegrationTests;

public class IntegrationTest
{
    protected readonly HttpClient TestClient;
    public IntegrationTest()
    {
        var appFactory = new WebApplicationFactory<Program>();

        TestClient =  appFactory.CreateClient();
    }
}