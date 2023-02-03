using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione.Mapper;

public class OrderMapper : IOrderMapper
{
    public Order Map(OrderListData orderListData)
    {
        Order order = new()
        {
            IoneRefId = orderListData.Id,
            IoneId = orderListData.IoneId,
            OrderTotal = orderListData.Total.GetDecimal(),
            OrderDate = orderListData.CreatedDate.GetDateTime(),
        };

        return order;
    }
}