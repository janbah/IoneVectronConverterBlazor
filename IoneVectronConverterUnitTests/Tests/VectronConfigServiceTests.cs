using ConnectorLib.Common.Config;
using Microsoft.Extensions.Configuration;

namespace IoneVectronConverterUnitTests;

public class VectronConfigServiceTests
{
    [Fact]
    public void GetVectronSettings_SettingFileIsValid_SettingsAreSet()
    {
        //Arrange
        var configuration = getConfiguration();
        var uut = new SettingService(configuration);

        //Act
        var result = uut.GetVectronSettings();

        //Assert
        Assert.True(result.ApiBaseAddress == "http://localhost:3001");
        Assert.True(result.OperatorCode == 2);
        Assert.True(result.AttributeNoForWebShop == 2);
        Assert.True(result.Operator == 3);
        Assert.True(result.ReceiptMediaNo == 4);
        Assert.True(result.TipDiscountNumber == 5);
        Assert.True(result.BranchAddressId == 10);
        Assert.True(result.IoneApiIdentifier == "apiIdentifier123");
        Assert.True(result.IoneApiToken == "123456789");
        Assert.True(result.PriceListAssignmentList[0].VectronPriceLevel == 1);
        Assert.True(result.PriceListAssignmentList[0].PriceListId == 2);
        Assert.True(result.TimerActive);
        Assert.True(result.WebServiceUrlPrefix =="urlPrefix");
    }
    
    [Fact]
    public void FunctionToTest_Prerequisites_Result()
    {
        //Arrange
    
        //Act
    
        //Assert
    }
    
    public IConfiguration getConfiguration()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(),"../../../Resources");
        Console.WriteLine(path);
        return new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();
    }
}