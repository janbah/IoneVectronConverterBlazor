using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverterTests;

public class MasterdataGenerator
{
    public MasterDataResponse createTestData()
    {
        MasterDataResponse response = new()
        {
            Taxes = getTaxes(),
            Departments = getDepartments(),
            SelWins = getSelWins(),
            PLUs = getPlus()
        };
        return response;
    }

    private PLU[] getPlus()
    {
        PLU plu1 = new()
        {
            PLUno = 1,
            Name1 = "name1",
            Name2 = "name2",
            Name3 = "name3",
            Attributes = "",
            Prices = new[]
            {
                new PriceData()
                {
                    Price = 20, Level = 1
                }
            },
            DepartmentNo = 1,
            SaleAllowed = true,
            SelectWin = new[] { 1, 2, 3 },
            TaxNo = 1,
            MainGroupA = 0,
            MainGroupB = 1,
            IsForWebShop = true
        };
        
        return new[] { plu1 };
    }

    private SelWin[] getSelWins()
    {
        SelWin selWin1 = new()
        {
            PLUs = new []{"1", "2", "3"},
            Name = "testSelWin1",
            Number = 1,
            SelectCompulsion = 1,
            SelectCount = 1,
            ZeroPriceAllowed = false
        };
        return new []{selWin1};
    }

    private Department[] getDepartments()
    {
        return new Department[] { };
    }

    private Tax[] getTaxes()
    {
        Tax tax1 = new()
        {
            TaxNo = 1,
            Name = "common",
            Rate = 19
        };
        Tax tax2 = new()
        {
            TaxNo = 2,
            Name = "reduced",
            Rate = 7
        };
        return new[] { tax1, tax2 };
    }
}