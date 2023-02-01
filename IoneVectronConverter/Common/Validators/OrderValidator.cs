using IoneVectronConverter.Common.Services;
using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Common.Validators;

public class OrderValidator : IOrderValidator
{
    private readonly IOrderService _orderService;

    public OrderValidator(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public bool IsValid(OrderListData orderItem)
    {
        return false;
    }
}