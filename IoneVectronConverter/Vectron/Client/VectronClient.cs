using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Vectron.Client;

public class VectronClient : IVectronClient
{
    //Todo: implement client
    public VPosResponse Send(OrderListData order)
    {
        throw new NotImplementedException();
    }

    public MasterDataResponse GetMasterData()
    {
        throw new NotImplementedException();
    }

    public Task<VPosResponse> SendReceipt(Receipt receipt)
    {
        throw new NotImplementedException();
    }
}