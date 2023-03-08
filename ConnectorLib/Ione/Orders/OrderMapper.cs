using ConnectorLib.Ione.Orders.Models;

namespace ConnectorLib.Ione.Orders;

public class OrderMapper : IOrderMapper
{
    public Order Map(OrderListData orderListData)
    {
        Order order = new()
        {
            IoneRefId = orderListData.Id,
            IoneId = orderListData.IoneId,
            // OrderTotal = orderListData.Total.GetDecimal(),
            // OrderDate = orderListData.CreatedDate.GetDateTime(),
        };

        return order;
    }
}