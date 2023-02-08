using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace IoneVectronConverterTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            Console.Write(Directory.GetCurrentDirectory());
            config.AddJsonFile(@"C:\Users\JanBahlmann\source\IoneVectronConverterBlazor\IoneVectronConverterIntegrationTests\Resources\appsettings.json");
        });
    }
}