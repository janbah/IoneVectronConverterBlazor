using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione.Validators;

public interface IOrderValidator
{
    bool IsValid(OrderListData orderItem);
}