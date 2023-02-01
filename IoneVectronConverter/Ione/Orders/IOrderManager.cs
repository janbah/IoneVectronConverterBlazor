using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione.Orders;

public interface IOrderManager
{
    void ProcessOrder(OrderListData order);
}