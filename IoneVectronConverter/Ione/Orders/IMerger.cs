using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Ione.Orders;

public interface IMerger
{
    Order Merge(Order orderData, VPosResponse response);
}