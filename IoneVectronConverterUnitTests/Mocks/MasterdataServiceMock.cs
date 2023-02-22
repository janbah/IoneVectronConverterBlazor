using System.Net;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Vectron.MasterData;
using Moq;
using Newtonsoft.Json;
using Order2VPos.Core.IoneApi.Items;

namespace IoneVectronConverterUnitTests.Mocks;

public class MasterdataServiceMock : Mock<IMasterdataService>
{
    
    public MasterdataServiceMock GetMasterdataMock()
    {
        Tax[] taxes = getDefaultTaxes();
        PLU[] plus = getDefaultPLUs();
        SelWin[] selWins = getDefaultSelWins();
        Department[] departments = getDefaulDepartments();

        MasterDataResponse response = new MasterDataResponse(taxes, plus, selWins, departments);
        
        Setup(x => x.GetMasterdataResponse()).Returns(response);
        return this;
    }
    
    public MasterdataServiceMock GetMasterdataMock(PLU[] plus)
    {
        Tax[] taxes = getDefaultTaxes();
        SelWin[] selWins = getDefaultSelWins();
        Department[] departments = getDefaulDepartments();

        MasterDataResponse response = new MasterDataResponse(taxes, plus, selWins, departments);
        
        Setup(x => x.GetMasterdataResponse()).Returns(response);
        return this;
    }

    private Department[] getDefaulDepartments()
    {
        Department department = new()
        {
            DepartmentNo = 1,
            Name = "TestDepartment"
        };

        return new[] { department };
    }

    private SelWin[] getDefaultSelWins()
    {
        SelWin selWin = new()
        {
            Name = "test",
            Number = 1,
            PLUs = new []{ "1" }
            
        };
        
        return new[] { selWin };
    }
    

    private PLU[] getDefaultPLUs()
    {
        PLU plu = new()
        {
            TaxNo = 1,
            IsForWebShop = true,
            SelectWin = new[] { 1 },
            Prices = getDefaultPrices(),
            MainGroupB = 1,
            Name1 = "",
            Name2 = "",
            Name3 = "",
            Attributes = "",
            DepartmentNo = 1,
            SaleAllowed = true,
            PLUno = 37894
        };

        return new[] { plu };
    }

    private Tax[] getDefaultTaxes()
    {
        Tax tax = new()
        {
            TaxNo = 1,
            Name = "common",
            Rate = 19,
        };

        return new[] { tax };
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