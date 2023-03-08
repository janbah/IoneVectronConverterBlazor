using ConnectorLib.Vectron.Masterdata.Models;

namespace ConnectorLib.Vectron.Masterdata.Services;

public interface IMasterdataService
{
    MasterDataResponse GetMasterdataResponse();

    void PersistMasterdataResponse();
}