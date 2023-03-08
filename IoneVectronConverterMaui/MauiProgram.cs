using ConnectorLib.Common.Datastoring;
using ConnectorLib.Vectron.Client;
using ConnectorLib.Vectron.Masterdata.Manager;
using ConnectorLib.Vectron.Masterdata.Models;
using ConnectorLib.Vectron.Masterdata.Repositories;
using ConnectorLib.Vectron.Masterdata.Services;
using IoneVectronConverterMaui.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace IoneVectronConverterMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });



         

            builder.Services.AddTransient<IVectronClient, VectronClient>();

            builder.Services.AddTransient<IRepository<PLU>, PluRepository>();
            builder.Services.AddTransient<IPluService, PluService>();

            builder.Services.AddTransient<IRepository<Tax>, TaxRepository>();
            builder.Services.AddTransient<ITaxService, TaxService>();

            builder.Services.AddTransient<IRepository<SelWin>, SelWinRepository>();
            builder.Services.AddTransient<ISelWinService, SelWinService>();

            builder.Services.AddTransient<IDepartmentService, DepartmentService>();

            builder.Services.AddTransient<IMasterdataReceiver, MasterdataReceiver>();
            builder.Services.AddMauiBlazorWebView();

            var path = @"C:\Users\JanBahlmann\source\IoneVectronConverterBlazor\IoneVectronConverterMaui";
            
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Configuration.AddConfiguration(config);
            
            builder.Services.AddMudServices();
            
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		    //builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}