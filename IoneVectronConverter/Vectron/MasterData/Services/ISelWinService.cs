using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Ione.Services;

public interface ISelWinService
{
    void StoreSelWinsIfNew(IEnumerable<SelWin> selWins);
}