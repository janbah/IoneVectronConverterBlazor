using ConnectorLib.Masterdata.Models;

namespace ConnectorLib.Masterdata.Services;

public interface ISelWinService
{
    void StoreSelWinsIfNew(IEnumerable<SelWin> selWins);
    IEnumerable<SelWin> GetAll();
}