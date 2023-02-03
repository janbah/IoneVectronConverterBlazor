using IoneVectronConverter.Ione.Datastoring;
using IoneVectronConverter.Ione.Mapper;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Ione.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;
    private readonly IOrderMapper _mapper;
    private readonly IMerger _merger;

    public OrderService(IRepository<Order> repository)
    {
        _repository = repository;
    }

    public void PersistOrderToDB(OrderListData orderData, VPosResponse response)
    {
        var orderToSave = mapOrderDataToOrder(orderData);
        var updatedOrderData = mergeOrderDataWithResponse(orderToSave, response);
        _repository.Insert(updatedOrderData);
    }

    public IQueryable<Order> GetOrders()
    {
        return _repository.Load();
    }

    public bool IsOrderNew(OrderListData orderListData)
    {
        throw new NotImplementedException();
    }

    private Order mapOrderDataToOrder(OrderListData updatedOrderData)
    {
        return _mapper.Map(updatedOrderData);
    }

    private Order mergeOrderDataWithResponse(Order orderToSave, VPosResponse response)
    {
        return _merger.Merge(orderToSave, response);
    }
}