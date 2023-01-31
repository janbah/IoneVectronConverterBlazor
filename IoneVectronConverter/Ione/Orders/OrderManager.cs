using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Models;
using IoneVectronConverter.Common.Services;
using IoneVectronConverter.Ione.Orders.Models;

namespace IoneVectronConverter.Ione.Orders
{
    public class OrderManager
    {

        private readonly IOrderService _orderService;

        public OrderManager(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void ProcessOrder(OrderItem order)
        {
            
        }
    }


}