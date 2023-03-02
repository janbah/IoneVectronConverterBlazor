using System.Net;
using IoneVectronConverter.Common;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Vectron.MasterData;
using IoneVectronConverterTests;
using IoneVectronConverterUnitTests.Mocks;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Order2VPos.Core.IoneApi.Items;

namespace IoneVectronConverterUnitTests;

public class MasterDataManagerTest
{

    private readonly IConfiguration _configuration;
    public MasterDataManagerTest()
    {
        _configuration = getConfiguration();
    }

    [Fact]
    public void SendPlus_IsForWebshop_DataIsPosted()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockSaveItemPostAsync();

        var plus = getDefaultPLUs();

        var pluServiceMock = new PluServiceMock().GetAllMock(plus);
        var taxServiceMock = new TaxServiceMock().GetAllMock();
        var selWinServiceMock = new SelWinServiceMock().GetAllMock();
        
        var sendAllItems = false;
        
        var uut = new MasterdataManager(ioneClientMock.Object, _configuration, pluServiceMock.Object, taxServiceMock.Object, selWinServiceMock.Object);

        //Act
        uut.SendPlus(sendAllItems);

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()),Times.Once);
        
    }    [Fact]
    
    public async Task SendPlus_IsNotForWebshop_ItemIsDeactivated()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockSaveItemPostAsync();
        
        var plus = getDefaultPLUs();
        
        plus[0].IsForWebShop = false;
        
        var pluServiceMock = new PluServiceMock().GetAllMock(plus);
        var taxServiceMock = new TaxServiceMock().GetAllMock();
        var selWinServiceMock = new SelWinServiceMock().GetAllMock();
        
        var sendAllItems = false;
        
        var uut = new MasterdataManager(ioneClientMock.Object, _configuration, pluServiceMock.Object, taxServiceMock.Object, selWinServiceMock.Object);

        //Act
        uut.SendPlus(sendAllItems);
        var sentItem = ioneClientMock.SentItems[0];

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()).Result, Times.Once);
        Assert.True(sentItem.ItemWebshopLink is false);
        
    }

    [Fact]
    public void SendPlus_SelWinMatch_DataIsPostedAsCondiment()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockSaveItemPostAsync()
            .MockSaveItemLinkLayerPostAsync();
        
        var plus = getDefaultPLUs();
        
        plus[0].IsForWebShop = true;
        plus[0].SelectWin[0] = 1;
        
        var pluServiceMock = new PluServiceMock().GetAllMock(plus);
        var taxServiceMock = new TaxServiceMock().GetAllMock();
        var selWinServiceMock = new SelWinServiceMock().GetAllMock();
        
        var sendAllItems = false;
        
        var uut = new MasterdataManager(ioneClientMock.Object, _configuration, pluServiceMock.Object, taxServiceMock.Object, selWinServiceMock.Object);
        
        //Act
        uut.SendPlus(sendAllItems);

        //Assert
        var linkLayer = ioneClientMock.ItemLinkLayers[0][0];
        
        ioneClientMock.Verify(x=>x.PostAsync(new Uri("SaveItem",UriKind.Relative), It.IsAny<StringContent>()), Times.Exactly(2));
        ioneClientMock.Verify(x=>x.PostAsync(new Uri("SaveItemLinkLayer",UriKind.Relative), It.IsAny<StringContent>()), Times.Exactly(1));
        
        Assert.True(linkLayer.APIObjectId == "1");
        Assert.True(linkLayer.BranchAddressId == 10);
        Assert.True(linkLayer.Id == 0);
        Assert.True(linkLayer.ItemID == 37);
        Assert.True(linkLayer.Name == "test");
        Assert.True(linkLayer.SelectionCounter == 1);
 
    }
    
    [Fact]
    public void SendPlus_PluIsAlreadyInIone_ItemIsSend()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockSaveItemPostAsync()
            .MockSaveItemLinkLayerPostAsync()
            ;
        
        var plus = getDefaultPLUs();
        
        plus[0].IsForWebShop = true;
        plus[0].SelectWin[0] = 1;
        plus[0].PLUno = 2;
        
        var pluServiceMock = new PluServiceMock().GetAllMock(plus);
        var taxServiceMock = new TaxServiceMock().GetAllMock();
        var selWinServiceMock = new SelWinServiceMock().GetAllMock();
        
        var sendAllItems = false;
        
        var uut = new MasterdataManager(ioneClientMock.Object, _configuration, pluServiceMock.Object, taxServiceMock.Object, selWinServiceMock.Object);

        //Act
        uut.SendPlus(sendAllItems);
        var sendItem = ioneClientMock.SentItems[0];

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()), Times.Exactly(1));
        Assert.True(sendItem.ItemWebshopLink);
        //mappingItem = 42
        Assert.True(sendItem.Id == 42);
        
    }
    
    [Fact]
    public void SendPlus_PluIsNotInIoneButHasWebshoplink_ItemIsSendWithId0()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockSaveItemPostAsync();
        
        var plus = getDefaultPLUs();
        
        plus[0].IsForWebShop = true;
        plus[0].SelectWin[0] = 1;
        plus[0].PLUno = 3;
        
        var pluServiceMock = new PluServiceMock().GetAllMock(plus);
        var taxServiceMock = new TaxServiceMock().GetAllMock();
        var selWinServiceMock = new SelWinServiceMock().GetAllMock();
        
        var sendAllItems = false;
        
        var uut = new MasterdataManager(ioneClientMock.Object, _configuration, pluServiceMock.Object, taxServiceMock.Object, selWinServiceMock.Object);

        //Act
        uut.SendPlus(sendAllItems);
        var sendItem = ioneClientMock.SentItems.FirstOrDefault();

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()), Times.Exactly(1));
        Assert.True(sendItem.ItemWebshopLink);
        Assert.True(sendItem.APIObjectId == "3");
        
        //mappingArticle = 0
        Assert.True(sendItem.Id == 0);
        
    }
    
    
    
    
    
    
    private IConfigurationRoot getConfiguration()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(),"../../../../IoneVectronConverterUnitTests/Resources");
        Console.WriteLine(path);
        return new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();
    }
    
    private PLU[] getDefaultPLUs()
    {
        PLU plu = new()
        {
            TaxNo = 1,
            IsForWebShop = true,
            SelectWin = new[] { 0 },
            Prices = getDefaultPrices(),
            MainGroupB = 1,
            Name1 = "",
            Name2 = "",
            Name3 = "",
            Attributes = "",
            DepartmentNo = 1,
            SaleAllowed = true,
            PLUno = 1
        };

        return new[] { plu };
    }
    
    private static PriceData[] getDefaultPrices()
    {
        return new PriceData[]{
            new ()
            {
                Price = 11,
                Level = 1
            }
        };
    }
}