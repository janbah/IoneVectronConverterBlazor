using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Common.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;

    public OrderService(IRepository<Order> repository)
    {
        _repository = repository;
    }


    public void Insert(OrderListData orderData, VPosResponse response)
    {
        var updatedOrderData = updateOrderDataWithResponse(response);
        var orderToSave = mapOrderDataToOrder(updatedOrderData);
        _repository.Insert(orderToSave);
    }

    private Order mapOrderDataToOrder(OrderListData updatedOrderData)
    {
        throw new NotImplementedException();
    }

    private OrderListData updateOrderDataWithResponse(VPosResponse response)
    {
        throw new NotImplementedException();
    }
}