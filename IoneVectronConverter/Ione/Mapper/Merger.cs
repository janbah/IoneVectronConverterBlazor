using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Ione.Mapper;

public class Merger : IMerger
{
    public Order Merge(Order order, VPosResponse response)
    {
        order.Status = response.IsError ? OrderStatus.Error : OrderStatus.Processed;
        order.ReceiptMainNo = response.ReceiptMainNo;
        order.ReceiptTotal = response.SubTotal;
        order.ReceiptUUId = response.UUId;

        order.Message = response.Message;
        order.VPosErrorNumber = response.VPosErrorNumber;
        order.IsCanceledOnPos = response.IsCanceled;
        
        return order;
    }
}