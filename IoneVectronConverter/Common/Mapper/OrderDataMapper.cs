using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;
using Order2VPos.Core.IoneApi;

namespace IoneVectronConverter.Common.Mapper;

public class OrderDataMapper : IOrderDataMapper
{
    private IOrderDataMapper _orderDataMapperImplementation;

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