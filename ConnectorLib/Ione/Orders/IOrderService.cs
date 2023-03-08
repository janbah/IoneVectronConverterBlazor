using ConnectorLib.Ione.Orders.Models;
using ConnectorLib.Vectron.Client;

namespace ConnectorLib.Ione.Orders;

public interface IOrderService
{
    long PersistOrderToDB(OrderListData orderData, VPosResponse response);

    IQueryable<Order> GetOrders();

    bool IsOrderNew(OrderListData orderListData);
}