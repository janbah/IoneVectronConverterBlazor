using ConnectorLib.Ione.Orders.Models;

namespace ConnectorLib.Ione.Orders;

public class OrderValidator : IOrderValidator
{
    private readonly IOrderService _orderService;

    public OrderValidator(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public bool IsValid(OrderListData orderItem)
    {
        //Todo: check for GC Ranges
        return _orderService.IsOrderNew(orderItem);
    }
}