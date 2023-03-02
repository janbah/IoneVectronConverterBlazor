using IoneVectronConverter.Ione.Orders;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverterUnitTests;

public class MergerTest
{
    [Fact]
    public void Merge_ResponseNoError_OrderStatusProcessed()
    {
        //Arrange
        Order order = new();

        VPosResponse response = new()
        {
            Message = "test message",
            IsCanceled = false,
            IsError = false,
            SubTotal = (decimal)13.20,
            ReceiptMainNo = 37894,
            UUId = "ORD37894",
            VPosErrorNumber = 3
        };

        Merger uut = new();

        //Act
        var result = uut.Merge(order, response);

        //Assert
        Assert.True(result.Status == OrderStatus.Processed);
        Assert.True(result.ReceiptMainNo == 37894);
        Assert.True(result.ReceiptTotal == (decimal)13.20);
        Assert.True(result.ReceiptUUId == "ORD37894");
    }
    
    [Fact]
    public void Merge_ResponseError_OrderStatusError()
    {
        //Arrange
        Order order = new();

        VPosResponse response = new()
        {
            Message = "test message",
            IsCanceled = false,
            IsError = true,
            SubTotal = (decimal)13.20,
            ReceiptMainNo = 37894,
            UUId = "ORD37894",
            VPosErrorNumber = 3
        };

        Merger uut = new();

        //Act
        var result = uut.Merge(order, response);

        //Assert
        Assert.True(result.Status == OrderStatus.Error);
        Assert.True(result.Message == "test message");
        Assert.True(result.VPosErrorNumber == 3);
        Assert.True(result.IsCanceledOnPos == false);
    }

}