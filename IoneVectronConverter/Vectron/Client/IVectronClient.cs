using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Vectron.Client;

public interface IVectronClient
{
    VPosResponse Send(OrderListData order);
}