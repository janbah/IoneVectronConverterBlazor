using IoneVectronConverter.Common.Masterdata.Services;
using IoneVectronConverter.Common.Models;
using Moq;

namespace IoneVectronConverterUnitTests.Mocks;

public class PluServiceMock : Mock<IPluService>
{
    private readonly PLU[] _plus;
    public PluServiceMock()
    {
    }

    public PluServiceMock GetAllMock(PLU[] plus)
    {
        Setup(s => s.GetAll()).Returns(plus.AsQueryable);
        return this;
    }
}