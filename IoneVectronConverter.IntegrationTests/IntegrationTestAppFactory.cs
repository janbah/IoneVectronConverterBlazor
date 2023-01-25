using Microsoft.AspNetCore.Mvc.Testing;

namespace IoneVectronConverter.IntegrationTests;

public class IntegrationTestAppFactory<TStartup>: WebApplicationFactory<TStartup> where TStartup : class
{
    
}