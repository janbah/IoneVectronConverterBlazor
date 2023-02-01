using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Common.Mapper;

public interface IOrderDataMapper
{
    Order Map(OrderListData orderListData);
}