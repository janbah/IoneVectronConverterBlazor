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


}