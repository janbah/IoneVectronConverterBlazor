using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Common.Services;

public interface IOrderService
{
    void PersistOrderToDB(OrderListData orderData, VPosResponse response);

    IQueryable<Order> GetOrders();

    bool IsOrderNew(OrderListData orderListData);
}