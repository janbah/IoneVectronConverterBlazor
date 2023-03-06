
using ConnectorLib.Client;
using ConnectorLib.Datastoring;
using ConnectorLib.Manager;
using ConnectorLib.Masterdata.Models;
using ConnectorLib.Masterdata.Repositories;
using ConnectorLib.Masterdata.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddTransient<IVectronClient, VectronClient>();

builder.Services.AddTransient<IRepository<PLU>, PluRepository>();
builder.Services.AddTransient<IPluService, PluService>();

builder.Services.AddTransient<IRepository<Tax>, TaxRepository>();
builder.Services.AddTransient<ITaxService, TaxService>();

builder.Services.AddTransient<IRepository<SelWin>, SelWinRepository>();
builder.Services.AddTransient<ISelWinService, SelWinService>();

builder.Services.AddTransient<IDepartmentService, DepartmentService>();

builder.Services.AddTransient<IMasterdataReceiver, MasterdataReceiver>();

var path = @"C:\Users\JanBahlmann\source\IoneVectronConverterBlazor\IoneVectronConverterMaui";
            
var config = new ConfigurationBuilder()
    .SetBasePath(path)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Configuration.AddConfiguration(config);



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