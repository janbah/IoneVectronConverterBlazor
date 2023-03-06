
using IoneVectronConverterMaui.Data;
using IoneVectronConverter.Common.Config;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Vectron.Mapper;
using IoneVectronConverter.Ione.Orders;
using IoneVectronConverter.Vectron.Client;
using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Ione.Categories;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Common.Masterdata.Services;
using IoneVectronConverter.Common.Masterdata.Repositories;
using IoneVectronConverter.Vectron.MasterData.Manager;

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

            builder.Services.AddTransient<ISettingService, SettingService>();
            builder.Services.AddTransient<Settings>();

            builder.Services.AddSingleton<IIoneClient, IoneClient>();
            builder.Services.AddTransient<ReceiptMapper>();


            builder.Services.AddTransient<IOrderValidator, OrderValidator>();
            builder.Services.AddTransient<IVectronClient, VectronClient>();
            builder.Services.AddTransient<IOrderManager, OrderManager>();

            builder.Services.AddTransient<IRepository<Order>, OrderRepository>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IMerger, Merger>();
            builder.Services.AddTransient<IOrderMapper, OrderMapper>();

            builder.Services.AddTransient<IRepository<IoneVectronConverter.Ione.Categories.Category>, CategoryRepository>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();

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

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}