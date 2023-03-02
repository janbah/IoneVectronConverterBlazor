using IoneVectronConverter.Ione.Orders;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;
using IoneVectronConverter.Vectron.Client;
using Moq;

namespace IoneVectronConverterUnitTests;

public class OrderManagerTests
{
    Mock<IVectronClient> _vectronClientMock = new();
    Mock<IOrderService> _orderSeviceMock  = new ();
    private Mock<IOrderValidator> _validatorMock = new();
    
    [Fact]
    public void ProcessOrder_ValidOrder_OrderIsStoredToDb()
    {
        //Arrange
        OrderListData order = new OrderListData();
        _validatorMock.Setup(v => v.IsValid(order)).Returns(true);
        OrderManager orderManager = new OrderManager(_orderSeviceMock.Object, _vectronClientMock.Object,_validatorMock.Object);

        //Act
        orderManager.ProcessOrder(order);

        //Assert
        _orderSeviceMock.Verify(r => r.PersistOrderToDB(It.IsAny<OrderListData>(), It.IsAny<VPosResponse>()));
    }
    

    [Fact]
    public void OrderManager_OrderIsValid_OrderIsSendToVectron()
    {
        //Arrange
        OrderListData order = new OrderListData();
        _validatorMock.Setup(v => v.IsValid(order)).Returns(true);
        OrderManager orderManager = new OrderManager(_orderSeviceMock.Object, _vectronClientMock.Object,_validatorMock.Object);

        //Act
        orderManager.ProcessOrder(order);

        //Assert
        _vectronClientMock.Verify(c => c.Send(order));
    }   
    
    [Fact]
    public void ProcessOrder_InValidOrder_OrderIsNotStoredToDb()
    {
        //Arrange
        OrderListData order = new OrderListData();
        _validatorMock.Setup(v => v.IsValid(order)).Returns(false);
        OrderManager orderManager = new OrderManager(_orderSeviceMock.Object, _vectronClientMock.Object,_validatorMock.Object);

        //Act
        orderManager.ProcessOrder(order);

        //Assert
        _orderSeviceMock.Verify(r => r.PersistOrderToDB(It.IsAny<OrderListData>(),It.IsAny<VPosResponse>()), Times.Never);
    }
    

    [Fact]
    public void ProcessOrder_InValidOrder_OrderIsNotSendToVectron()
    {
        //Arrange
        OrderListData order = new OrderListData();
        _validatorMock.Setup(v => v.IsValid(order)).Returns(false);
        OrderManager orderManager = new OrderManager(_orderSeviceMock.Object, _vectronClientMock.Object,_validatorMock.Object);

        //Act
        orderManager.ProcessOrder(order);

        //Assert
        _vectronClientMock.Verify(c => c.Send(It.IsAny<OrderListData>()), Times.Never);
    }

}
