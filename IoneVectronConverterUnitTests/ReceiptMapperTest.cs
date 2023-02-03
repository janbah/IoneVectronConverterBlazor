using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.Mapper;
using Newtonsoft.Json;

namespace IoneVectronConverterUnitTests;

public class ReceiptMapperTest
{
    [Fact]
    public void ImportTestData_IsValid()
    {
        //Arrange
        string path = Path.Combine(@"..\..\..\Resources","OrderListShort.json");
        string jsonInput = File.ReadAllText(path);

        //Act
        var order = JsonConvert.DeserializeObject<OrderListData>(jsonInput);

        //Assert
        Assert.True(order.Id ==50716);
        Assert.True(order.OrderItemList.Length == 2);
        Assert.True(order.OrderItemList[0].ItemList.Length == 1);
    }
    
    [Fact]
    public void ReceiptMapper_OrderIsValid_ReceiptIsValid()
    {
        //Arrange
        string path = Path.Combine(@"..\..\..\Resources","OrderListShort.json");
        string jsonInput = File.ReadAllText(path);
        var order = JsonConvert.DeserializeObject<OrderListData>(jsonInput);

        var uut = new ReceiptMapper();
        
        //Act
        var result = uut.Map(order);

        //Assert
        Assert.True(result.Gc == 212);
        Assert.True(result.GcText == "AUF-000000041-21 - Testbestellung im Shop");
        Assert.True(1 == result.Discounts.Count);
        Assert.True(1 == result.Plus.Count);
    }
    
    [Fact]
    public void ReceiptMapper_orderItemIsValid_ReceiptPluIsValid()
    {
        //Arrange
        string path = Path.Combine(@"..\..\..\Resources","OrderListShort.json");
        string jsonInput = File.ReadAllText(path);
        var order = JsonConvert.DeserializeObject<OrderListData>(jsonInput);

        var uut = new ReceiptMapper();
        
        //Act
        var result = uut.Map(order);
        var plu = result.Plus[0];
        
        //Assert
        Assert.True(plu.Number == 108);
        Assert.True(plu.OverridePriceFactor == Convert.ToDecimal(1.00));
        Assert.True(plu.OverridePriceValue == Convert.ToDecimal(12.50));
        Assert.True(plu.Quantity == 1);
    }   
    
    [Fact]
    public void ReceiptMapper_orderItemItemIsValid_ReceiptPluAdditianalPluIsValid()
    {
        //Arrange
        string path = Path.Combine(@"..\..\..\Resources","OrderListShort.json");
        string jsonInput = File.ReadAllText(path);
        var order = JsonConvert.DeserializeObject<OrderListData>(jsonInput);

        var uut = new ReceiptMapper();
        
        //Act
        var result = uut.Map(order);
        var additionalPlu = result.Plus[0].AdditionalPlus[0];
        
        //Assert
        Assert.True(additionalPlu.Number == 812);
        Assert.True(additionalPlu.OverridePriceFactor == 1);
    }  
}