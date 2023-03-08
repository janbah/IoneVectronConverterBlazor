using ConnectorLib.Ione.Orders.Models;

namespace ConnectorLib.Ione.Orders;

public interface IOrderManager
{
    Task ProcessOrder(OrderListData order);
}