using ConnectorLib.Masterdata.Models;

namespace ConnectorLib.Masterdata.Services;

public interface IMasterdataService
{
    MasterDataResponse GetMasterdataResponse();

    void PersistMasterdataResponse();
}