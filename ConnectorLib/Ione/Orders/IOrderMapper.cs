using ConnectorLib.Ione.Orders.Models;

namespace ConnectorLib.Ione.Orders;

public interface IOrderMapper
{
    Order Map(OrderListData orderListData);
}