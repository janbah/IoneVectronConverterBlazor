using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Vectron.MasterData;
using Moq;

namespace IoneVectronConverterUnitTests.Mocks;

public class SelWinServiceMock : Mock<ISelWinService>
{
    private readonly PLU[] _plus;
    public SelWinServiceMock()
    {
    }

    public SelWinServiceMock GetAllMock()
    {
        SelWin selWin = new() { };
        var selwins = getDefaultSelWins;
        
        Setup(s => s.GetAll()).Returns(selwins);
        return this;
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
}