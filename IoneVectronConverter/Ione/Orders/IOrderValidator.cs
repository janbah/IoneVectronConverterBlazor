using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione.Orders;

public interface IOrderValidator
{
    bool IsValid(OrderListData orderItem);
}