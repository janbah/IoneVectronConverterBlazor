using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Vectron.MasterData;
using Moq;

namespace IoneVectronConverterUnitTests.Mocks;

public class PluServiceMock : Mock<PluService>
{
    private readonly PLU[] _plus;
    public PluServiceMock(PLU[] plus)
    {
        _plus = plus;
    }

    public PluServiceMock GetAllMock()
    {
        Setup(s => s.GetAll()).Returns(_plus.AsQueryable);
        return this;
    }
}