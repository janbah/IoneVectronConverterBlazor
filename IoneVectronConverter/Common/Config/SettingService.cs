using System.Text.Json;
using System.Text.Json.Serialization;

namespace IoneVectronConverter.Common;

public class SettingService
{
    IConfiguration config;

     private readonly Settings _settings;

// Get values from the config given their key and their target type.

     public SettingService()
     {
         config = new ConfigurationBuilder()
             .AddJsonFile("/home/wir/Projekte/IoneVectronConverterBlazor/IoneVectronConverterBlazor/appsettings.json")
             .AddEnvironmentVariables()
             .Build();
         
         
         _settings = config.Get<Settings>();
     }

     public Settings GetSettings()
     {
         return _settings;
     }

     public void SetSettings()
     {
         var jsonWriteOptions = new JsonSerializerOptions()
         {
             WriteIndented = true
         };
         jsonWriteOptions.Converters.Add(new JsonStringEnumConverter());

         var newJson = JsonSerializer.Serialize(_settings, jsonWriteOptions);
             
         
         var appSettingsPath = @"/home/wir/Projekte/IoneVectronConverterBlazor/IoneVectronConverterBlazor/appsettings.json";
         //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
         File.WriteAllText(appSettingsPath, newJson);
     }

}