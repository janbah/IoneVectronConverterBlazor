using ConnectorLib.Ione.Orders.Models;
using ConnectorLib.Vectron.Masterdata.Models;

namespace ConnectorLib.Vectron.Client;

public interface IVectronClient
{

    MasterDataResponse GetMasterData();

    Task<VPosResponse> Send(OrderListData order);
}