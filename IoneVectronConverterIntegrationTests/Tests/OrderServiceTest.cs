using IoneVectronConverter.Ione.Datastoring;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Vectron;
using IoneVectronConverterTests;
using Microsoft.Extensions.DependencyInjection;

namespace IoneVectrinConverterTests;

public class OrderServiceTest
{

    private readonly CustomWebApplicationFactory<Program> _webApplicationFactory;

    public OrderServiceTest()
    {
         _webApplicationFactory= new CustomWebApplicationFactory<Program>();
        // resetDataBase();

    }

    [Fact]
    public void GetOrders_ReturnsOneOrder()
    {
        //Arrange
        IOrderService _sut = _webApplicationFactory.Services.GetRequiredService<IOrderService>();
    
        //Act
        var result = _sut.GetOrders();
    
        //Assert
        //Assert.True(result.Count() == 1);
        Assert.True(result.First().Id == 1);
        Assert.True(result.First().IoneRefId == 37894);
    }
    
    [Fact]
    public void PersistOrder_OrderIsSavedToDB()
    {
        //Arrange
        var webApplicationFactory = new CustomWebApplicationFactory<Program>();
        IRepository<Order> repo = _webApplicationFactory.Services.GetRequiredService<IRepository<Order>>();

        IOrderService _sut = webApplicationFactory.Services.GetRequiredService<IOrderService>();
        

        OrderListData order = new()
        {
            IoneId = "IO37895",
            CustomerNotes = "test order 2",
            OrderItemList = new OrderItemListItem[2],
            Id = 23,
            Total = "19,11",
            CreatedDate = "2023-02-06",

        };

        VPosResponse response = new()
        {
            IsError = false,
            IsCanceled = false,
            VPosErrorNumber = 1,
            Message = "response message",
            ReceiptMainNo = 42,
            SubTotal = (decimal)22.10,
            UUId = "uuid response"
        };

        //Act
        
        var id = _sut.PersistOrderToDB(order,response);

        //Assert
        var result =  _sut.GetOrders();
        var count = result.Count();
        
        Assert.True(count > 1);
        
        repo.Delete(Convert.ToInt32(id));
        //resetDataBase();

    }

    private void resetDataBase()
    {
        string path = @"..\..\..\Resources";
        string databaseName = @"IoneVectronTest.sqlite";
        string databaseBackupName = @"IoneVectronTestBackup.sqlite";
        
        File.Move(Path.Combine(path, databaseBackupName),Path.Combine(path,databaseName), true);
    }
}