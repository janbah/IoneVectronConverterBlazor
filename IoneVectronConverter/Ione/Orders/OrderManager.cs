using Order2VPos.Core.IoneApi.Orders;
using Order2VPos.Core.VPosClient;

namespace IoneVectronConverter.IoneClient.Orders
{
    public class OrderManager
    {

        private readonly IIoneClient _ioneClient;
        private readonly IRepository _repository;

        public OrderManager(IIoneClient ioneClient)
        {
            _ioneClient = ioneClient;
        }

        public void DoWork()
        {
            IEnumerable<OrderListData> newOrders = getNewOrders();

            foreach (var order in newOrders)
            {
                storeOrderInDb(order);
                sendOrderToVectron(order);
            }
        }

        private void sendOrderToVectron(OrderListData order)
        {
            VPosResponse response = new VPosResponse();

            if (response.IsError)
            {
                return;
            }
            else
            {
                
            }

            updateDatabaseEntry();

        }

        private void updateDatabaseEntry()
        {
            throw new NotImplementedException();
        }

        private void storeOrderInDb(OrderListData order)
        {
            
            throw new NotImplementedException();
        }

        private object getNewOrdersFromIoneClient()
        {
            throw new NotImplementedException();
        }

        private void processNewOrders(object order)
        {
            
            
        }

        private IEnumerable<OrderListData> getNewOrders()
        {
            IEnumerable<OrderListData> ordersToSend = new List<OrderListData>();
            var newIoneOrders = getNewOrdersFromIoneClient();
            var storedOrders = getStoredOrders();

            return ordersToSend;
        }

        private object getStoredOrders()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IRepository
    {
    }
}