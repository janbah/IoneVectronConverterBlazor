using IoneVectronConverter.Ione.Categories;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.MasterData.Models;
using IoneVectronConverter.Vectron.Models;

namespace IoneVectronConverter.Vectron.Client;

public interface IVectronClient
{
    Task<VPosResponse> Send(OrderListData order);

    MasterDataResponse GetMasterData();

    public Task<VPosResponse> SendReceipt(Receipt receipt);
    IEnumerable<ItemCategory> GetCategories();
}