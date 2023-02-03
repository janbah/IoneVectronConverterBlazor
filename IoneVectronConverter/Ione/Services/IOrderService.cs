using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Ione.Services;

public interface IOrderService
{
    void PersistOrderToDB(OrderListData orderData, VPosResponse response);

    IQueryable<Order> GetOrders();

    bool IsOrderNew(OrderListData orderListData);
}