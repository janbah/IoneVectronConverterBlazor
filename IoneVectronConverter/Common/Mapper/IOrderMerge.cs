using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Common.Mapper;

public interface IOrderMerge
{
    Order Merge(Order orderData, VPosResponse response);
}