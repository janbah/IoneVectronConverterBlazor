using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Common.Validators;

public interface IOrderValidator
{
    bool IsValid(OrderListData orderItem);
}