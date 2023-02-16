using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.MasterData;
using IoneVectronConverter.Vectron.Models;
using Order2VPos.Core.IoneApi.ItemCategories;

namespace IoneVectronConverter.Vectron.Client;

public interface IVectronClient
{
    Task<VPosResponse> Send(OrderListData order);

    MasterDataResponse GetMasterData();

    public Task<VPosResponse> SendReceipt(Receipt receipt);
    IEnumerable<ItemCategory> GetCategories();
}