using IoneVectronConverter.IoneClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using IoneVectronConverterBlazor.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddHttpClient<IoneClient>("ioneClient",client =>
{
    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    //     "Bearer",
    //     AppSettings.Default.IoneApiToken);
    // client.DefaultRequestHeaders.Add("Identifier", AppSettings.Default.IoneApiIdentifier);
    // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    client.BaseAddress = new Uri("http://localhost:3001");
});


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