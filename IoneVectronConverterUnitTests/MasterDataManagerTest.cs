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
        Console.Write(Directory.GetCurrentDirectory());
        
        //Arrange
        var masterDataRepositoryMock = new Mock<IMasterdataService>();
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockPostAsync();

        MasterDataResponse masterdata = getMasterDataForMock();
        masterDataRepositoryMock.Setup(s => s.GetMasterdataResponse()).Returns(masterdata);
        
        var sendAllItems = false;
        
        var uut = new LegacyMasterdataManager(ioneClientMock.Object, masterDataRepositoryMock.Object, _configuration);

        //Act
        uut.SendPlus(sendAllItems);

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()));
        
    }

    private MasterDataResponse getMasterDataForMock()
    {
        int[] selectWin = { };
        PLU plu = new()
        {
            MainGroupB = 1,
            IsForWebShop = true,
            SelectWin = selectWin,
            Prices = getPrices()
        };

        Tax tax = new()
        {
            TaxNo = 1,
            Name = "common",
            Rate = 19,
        };
        
        
        SelWin[] selWin = { };
        
        MasterDataResponse masterData = new()
        {
            PLUs = new []{plu},
            Taxes = new[]{tax},
            SelWins = selWin
        };
        return masterData;
    }

    private static PriceData[] getPrices()
    {
        return new PriceData[]{
            new ()
            {
                Price = 11,
                Level = 1
            }
        };
    }
    
    private IConfigurationRoot getConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\Users\JanBahlmann\source\IoneVectronConverterBlazor\IoneVectronConverterUnitTests\Resources\appsettings.json")
            .Build();
    }
}