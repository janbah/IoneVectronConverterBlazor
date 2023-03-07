using ConnectorLib.Masterdata.Models;

namespace ConnectorLib.Client;

public interface IVectronClient
{

    MasterDataResponse GetMasterData();

}