using IoneVectronConverter.Common;
using IoneVectronConverter.Common.Config;
using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Masterdata.Repositories;
using IoneVectronConverter.Common.Masterdata.Services;
using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Common.Worker;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Categories;
using IoneVectronConverter.Ione.Orders;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.Client;
using IoneVectronConverter.Vectron.Mapper;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

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

builder.Services.AddTransient<IRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddTransient<IRepository<PLU>, PluRepository>();
builder.Services.AddTransient<IPluService, PluService>();

builder.Services.AddTransient<IRepository<Tax>, TaxRepository>();
builder.Services.AddTransient<ITaxService, TaxService>();

builder.Services.AddTransient<IRepository<SelWin>, SelWinRepository>();
builder.Services.AddTransient<ISelWinService, SelWinService>();


builder.Services.AddHttpClient<IIoneClient, IoneClient>("ioneClient",client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ioneClient"]);
});

builder.Services.AddSingleton<IWorker, Worker>();


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