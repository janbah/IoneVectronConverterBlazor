using ConnectorLib.Common.Datastoring;
using ConnectorLib.Ione.Categories;
using ConnectorLib.Ione.Client;
using ConnectorLib.Vectron.Client;
using ConnectorLib.Vectron.Masterdata.Manager;
using ConnectorLib.Vectron.Masterdata.Models;
using ConnectorLib.Vectron.Masterdata.Repositories;
using ConnectorLib.Vectron.Masterdata.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices();


var basepath = Directory.GetCurrentDirectory();
            
var config = new ConfigurationBuilder()
    .SetBasePath(basepath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Configuration.AddConfiguration(config);

builder.Services.AddTransient<IIoneClient, IoneClient>();

builder.Services.AddTransient<CategoryMapper>();

builder.Services.AddTransient<ICategoryManager, CategoryManager>();

builder.Services.AddTransient<IRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();


builder.Services.AddTransient<IVectronClient, VectronClient>();

builder.Services.AddTransient<IRepository<PLU>, PluRepository>();
builder.Services.AddTransient<IPluService, PluService>();

builder.Services.AddTransient<IRepository<Tax>, TaxRepository>();
builder.Services.AddTransient<ITaxService, TaxService>();

builder.Services.AddTransient<IRepository<SelWin>, SelWinRepository>();
builder.Services.AddTransient<ISelWinService, SelWinService>();

builder.Services.AddTransient<IDepartmentService, DepartmentService>();

builder.Services.AddTransient<IMasterdataReceiver, MasterdataReceiver>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();