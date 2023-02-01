using IoneVectronConverter.Common.Services;
using IoneVectronConverter.Common.Validators;
using IoneVectronConverter.Ione.Orders.Models;
using Moq;

namespace IoneVectronConverterUnitTests;

public class OrderValidatorTest
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly OrderListData orderData;

    public OrderValidatorTest()
    {
        _orderServiceMock = new Mock<IOrderService>();
        orderData = new();
    }


    
    [Fact]
    public void Validator_OrderIsNewAndInGcRange_OrderIsValid()
    {
        //Arrange
        _orderServiceMock.Setup(s => s.IsOrderNew(orderData)).Returns(true);
        
        OrderValidator uut = new(_orderServiceMock.Object);

        //Act
        var result =uut.IsValid(orderData);

        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void Validator_OrderIsNotNewDbAndInGcRange_OrderNotValid()
    {
        //Arrange
        
        _orderServiceMock.Setup(s => s.IsOrderNew(orderData)).Returns(false);
        
        OrderValidator uut = new(_orderServiceMock.Object);

        //Act
        var result =uut.IsValid(orderData);

        //Assert
        Assert.False(result);
    }

}