using Order2VPos.Core.IoneApi.Orders;

namespace IoneVectronConverter.IoneClient;

public interface IIoneClient
{
    Task<OrderListResponse> GetOrdersAsync(DateTime from, DateTime to);
}