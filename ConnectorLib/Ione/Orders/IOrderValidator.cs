using ConnectorLib.Ione.Orders.Models;

namespace ConnectorLib.Ione.Orders;

public interface IOrderValidator
{
    bool IsValid(OrderListData orderItem);
}