using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione;

public interface IIoneClient
{
    Task<OrderListResponse> GetOrdersAsync(DateTime from, DateTime to);
}