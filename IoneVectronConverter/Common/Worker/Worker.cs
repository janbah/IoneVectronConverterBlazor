using IoneVectronConverter.Common.Services;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Orders;

namespace IoneVectronConverter.Common.Worker;

public class Worker
{
    private readonly IIoneClient _ioneClient;
    private readonly IOrderManager _orderManager;
    private readonly DateTime _from;
    private readonly DateTime _to;

    public Worker(IIoneClient ioneClient, IOrderManager orderManager)
    {
        _ioneClient = ioneClient;
        _orderManager = orderManager;
        _from = DateTime.Now.AddHours(-3);
        _to = DateTime.Now.AddHours(3);
    }

    public async Task ProcessOrdesfromIone()
    {
        var orders = await _ioneClient.GetOrdersAsync(_from, _to);

        foreach (var order in orders.Data)
        {
            _orderManager.ProcessOrder(order);
        }
    }
}