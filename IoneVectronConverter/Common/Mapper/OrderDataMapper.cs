using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Ione.Orders.Models;
using Order2VPos.Core.IoneApi;

namespace IoneVectronConverter.Common.Mapper;

public class OrderDataMapper
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