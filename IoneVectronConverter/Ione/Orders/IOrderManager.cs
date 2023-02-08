using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione.Orders;

public interface IOrderManager
{
    Task ProcessOrder(OrderListData order);
}