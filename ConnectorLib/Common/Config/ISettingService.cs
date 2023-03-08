namespace ConnectorLib.Common.Config;

public interface ISettingService
{
    VectronSettings GetVectronSettings();
    void SetSettings();
}