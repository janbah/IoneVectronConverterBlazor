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
            .MockPostAsync();

        var plus = getDefaultPLUs();
        
        var masterdataMock = new MasterdataServiceMock()
            .GetMasterdataMock(plus);
        
        var sendAllItems = false;
        
        var uut = new LegacyMasterdataManager(ioneClientMock.Object, masterdataMock.Object, _configuration);

        //Act
        uut.SendPlus(sendAllItems);

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()),Times.Once);
        
    }    [Fact]
    
    public void SendPlus_IsNotForWebshop_DataIsPosted()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockPostAsync();
        
        var plus = getDefaultPLUs();
        
        plus[0].IsForWebShop = false;
        
        var masterdataMock = new MasterdataServiceMock()
            .GetMasterdataMock(plus);
        
        var sendAllItems = false;
        
        var uut = new LegacyMasterdataManager(ioneClientMock.Object, masterdataMock.Object, _configuration);

        //Act
        uut.SendPlus(sendAllItems);

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()), Times.Never);
        
    }

    [Fact]
    public void SendPlus_SelWinMatch_DataIsPostedAsCondiment()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockPostAsync();
        
        var plus = getDefaultPLUs();
        
        plus[0].IsForWebShop = true;
        plus[0].SelectWin[0] = 1;
        
        var masterdataMock = new MasterdataServiceMock()
            .GetMasterdataMock(plus);
        
        var sendAllItems = false;
        
        var uut = new LegacyMasterdataManager(ioneClientMock.Object, masterdataMock.Object, _configuration);

        //Act
        uut.SendPlus(sendAllItems);

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()), Times.Exactly(3));
        
    }
    
    [Fact]
    public void SendPlus_PluIsAlreadyInIone_DontEvenKnow()
    {
        //Arrange
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockPostAsync();
        
        var plus = getDefaultPLUs();
        
        plus[0].IsForWebShop = true;
        plus[0].SelectWin[0] = 1;
        
        var masterdataMock = new MasterdataServiceMock()
            .GetMasterdataMock(plus);
        
        var sendAllItems = false;
        
        var uut = new LegacyMasterdataManager(ioneClientMock.Object, masterdataMock.Object, _configuration);

        //Act
        uut.SendPlus(sendAllItems);

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()), Times.Exactly(3));
        
    }
    
    
    
    
    
    
    private IConfigurationRoot getConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\Users\JanBahlmann\source\IoneVectronConverterBlazor\IoneVectronConverterUnitTests\Resources\appsettings.json")
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