using ConnectorLib.Ione.Orders.Models;
using ConnectorLib.Vectron.Client;

namespace ConnectorLib.Ione.Orders;

public interface IMerger
{
    Order Merge(Order orderData, VPosResponse response);
}