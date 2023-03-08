using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace ConnectorLib.Common.Config;

public class SettingService : ISettingService
{
    IConfiguration _config;

     private readonly VectronSettings _vectronSettings;

     
     public SettingService(IConfiguration configuration)
     {
         _config = configuration;
         _vectronSettings = _config.GetSection("Vectron").Get<VectronSettings>();
     }

     public VectronSettings GetVectronSettings()
     {
         return _vectronSettings;
     }
     
     

     public void SetSettings()
     {
         var jsonWriteOptions = new JsonSerializerOptions()
         {
             WriteIndented = true
         };
         jsonWriteOptions.Converters.Add(new JsonStringEnumConverter());

         var newJson = JsonSerializer.Serialize(_vectronSettings, jsonWriteOptions);

         var path = _config["AppSettingPath"];
         //var appSettingsPath = @"/home/wir/Projekte/IoneVectronConverterBlazor/IoneVectronConverterBlazor/appsettings.json";
         var appSettingsPath = Path.Combine(path, "appsettings.json");
         File.WriteAllText(appSettingsPath, newJson);
     }

}