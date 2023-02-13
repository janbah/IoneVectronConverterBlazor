using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Orders;

namespace IoneVectronConverter.Common.Worker;

public interface IWorker
{
    Task ProcessOrdesfromIone();
}

public class Worker : BackgroundService, IWorker
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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessOrdesfromIone();
            await Task.Delay(1000, stoppingToken);
        }
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