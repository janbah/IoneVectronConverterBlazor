using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Vectron.Client;

public interface IVectronClient
{
    VPosResponse Send(OrderListData order);

    MasterDataResponse GetMasterData();

    public Task<VPosResponse> SendReceipt(Receipt receipt);
}