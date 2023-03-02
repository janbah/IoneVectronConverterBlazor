using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Ione.Orders;

public interface IOrderService
{
    long PersistOrderToDB(OrderListData orderData, VPosResponse response);

    IQueryable<Order> GetOrders();

    bool IsOrderNew(OrderListData orderListData);
}