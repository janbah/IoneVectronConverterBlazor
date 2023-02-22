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
        var ioneClientMock = new IoneClientMock()
            .MockGetItems()
            .MockPostAsync();

        var masterdataMock = new MasterdataServiceMock()
            .GetMasterdataMock();
        
        var sendAllItems = false;
        
        var uut = new LegacyMasterdataManager(ioneClientMock.Object, masterdataMock.Object, _configuration);

        //Act
        uut.SendPlus(sendAllItems);

        //Assert
        ioneClientMock.Verify(x=>x.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>()));
        
    }


    
    private IConfigurationRoot getConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\Users\JanBahlmann\source\IoneVectronConverterBlazor\IoneVectronConverterUnitTests\Resources\appsettings.json")
            .Build();
    }
}