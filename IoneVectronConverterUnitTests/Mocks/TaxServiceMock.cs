using IoneVectronConverter.Common.Masterdata.Services;
using IoneVectronConverter.Common.Models;
using Moq;

namespace IoneVectronConverterUnitTests.Mocks;

public class TaxServiceMock : Mock<ITaxService>
{

    public TaxServiceMock GetAllMock()
    {
        Tax[] taxes = getDefaultTaxes();
        Setup(s => s.GetAll()).Returns(taxes.AsQueryable);
        return this;
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