using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione.Orders;

public interface IOrderMapper
{
    Order Map(OrderListData orderListData);
}