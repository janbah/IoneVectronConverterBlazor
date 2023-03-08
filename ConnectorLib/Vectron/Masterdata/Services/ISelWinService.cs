using ConnectorLib.Vectron.Masterdata.Models;

namespace ConnectorLib.Vectron.Masterdata.Services;

public interface ISelWinService
{
    void StoreSelWins(IEnumerable<SelWin> selWins);
    IEnumerable<SelWin> GetAll();
}