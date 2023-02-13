using IoneVectronConverter.Common;
using IoneVectronConverter.Common.Worker;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Datastoring;
using IoneVectronConverter.Ione.Mapper;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Orders;
using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Ione.Validators;
using IoneVectronConverter.Vectron.Client;
using IoneVectronConverter.Vectron.Mapper;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddTransient<Settings>();

builder.Services.AddSingleton<IIoneClient, IoneClient>();

builder.Services.AddTransient<IOrderMapper, OrderMapper>();
builder.Services.AddTransient<IMerger, Merger>();

builder.Services.AddTransient<IOrderValidator, OrderValidator>();
builder.Services.AddTransient<IVectronClient, VectronClient>();
builder.Services.AddTransient<IOrderManager, OrderManager>();

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ReceiptMapper>();

builder.Services.AddTransient<IRepository<Order>, OrderRepository>();

//builder.Services.AddHttpClient("ioneClient",client =>
builder.Services.AddHttpClient<IIoneClient, IoneClient>("ioneClient",client =>
{
    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    //     "Bearer",
    //     AppSettings.Default.IoneApiToken);
    // client.DefaultRequestHeaders.Add("Identifier", AppSettings.Default.IoneApiIdentifier);
    // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    client.BaseAddress = new Uri(builder.Configuration["ioneClient"]);
});

builder.Services.AddTransient<IoneClient>();
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