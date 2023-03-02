using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Common.Masterdata.Services;

public interface ISelWinService
{
    void StoreSelWinsIfNew(IEnumerable<SelWin> selWins);
    IEnumerable<SelWin> GetAll();
}