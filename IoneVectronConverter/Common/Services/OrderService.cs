using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Mapper;
using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Common.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;
    private readonly IOrderDataMapper _mapper; 

    public OrderService(IRepository<Order> repository)
    {
        _repository = repository;
    }

    public void Insert(OrderListData orderData, VPosResponse response)
    {
        var orderToSave = mapOrderDataToOrder(orderData);
        var updatedOrderData = mergeOrderDataWithResponse(orderToSave, response);
        _repository.Insert(updatedOrderData);
    }

    private Order mapOrderDataToOrder(OrderListData updatedOrderData)
    {
        return _mapper.Map(updatedOrderData);
    }

    private Order mergeOrderDataWithResponse(Order orderToSave, VPosResponse response)
    {
        throw new NotImplementedException();
    }
}