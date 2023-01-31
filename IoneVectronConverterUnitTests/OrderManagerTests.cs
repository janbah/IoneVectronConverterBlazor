using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Services;
using IoneVectronConverter.Ione.Orders;
using IoneVectronConverter.Ione.Orders.Models;
using Moq;

namespace IoneVectronConverterUnitTests;

public class OrderManagerTests
{
    [Fact]
    public void ProcessOrder_ValidOrder_OrderIsStoredToDb()
    {
        //Arrange
        OrderItem order = new OrderItem();
        OrderManager orderManager = new OrderManager();
        var orderRepoMock  = new Mock<IOrderService>();

        //Act
        orderManager.ProcessOrder(order);

        //Assert
        orderRepoMock.Verify(or =>or.Insert(order));
    }
}