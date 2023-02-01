using IoneVectronConverter.Common.Mapper;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron;
using Order2VPos.Core.IoneApi;

namespace IoneVectronConverterUnitTests;

public class OrderDataMapperTest
{
    [Fact]
    public void Mapper_Orderlistdata_ValidOrder()
    {
        //Arrange
        OrderDataMapper uut = new OrderDataMapper();

        VPosResponse response = new VPosResponse();
        OrderListData data = new OrderListData()
        {
            Total = "10,50",
            CustomerNotes = "Test Note",
            TableId = "42",
            Id = 37,
            IoneId = "I3742",
            CreatedDate = "2023-01-01"
        };

        //Act
        var result = uut.Map(data);

        //Assert
        Assert.True(result.IoneRefId == data.Id);
        Assert.True(result.IoneId == data.IoneId);
        Assert.True(result.OrderTotal == (decimal)10.42);
        Assert.True(result.OrderDate == new DateTime(2023,01,01));
    }
}

