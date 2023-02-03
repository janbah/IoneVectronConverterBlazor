using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione.Mapper;

public interface IOrderMapper
{
    Order Map(OrderListData orderListData);
}