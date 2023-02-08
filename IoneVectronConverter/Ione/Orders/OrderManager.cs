using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Ione.Validators;
using IoneVectronConverter.Vectron.Client;

namespace IoneVectronConverter.Ione.Orders
{
    public class OrderManager : IOrderManager
    {

        private readonly IOrderService _orderService;
        private readonly IVectronClient _vectronClient;
        private readonly IOrderValidator _orderValidator;

        public OrderManager(IOrderService orderService, IVectronClient vectronClient, IOrderValidator orderValidator)
        {
            _orderService = orderService;
            _vectronClient = vectronClient;
            _orderValidator = orderValidator;
        }

        public async Task ProcessOrder(OrderListData order)
        {
            if (_orderValidator.IsValid(order) is false)
            {
                return;
            }
            
            var result = await _vectronClient.Send(order);
            _orderService.PersistOrderToDB(order, result);
        }
    }


}