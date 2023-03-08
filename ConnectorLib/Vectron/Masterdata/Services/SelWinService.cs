using ConnectorLib.Common.Datastoring;
using ConnectorLib.Vectron.Masterdata.Models;

namespace ConnectorLib.Vectron.Masterdata.Services;

public class SelWinService : ISelWinService
{
    private readonly IRepository<SelWin> _repository;
    public SelWinService(IRepository<SelWin> selWinRepository)
    {
        _repository = selWinRepository;
    }
    public SelWinService()
    {
    }

    public void StoreSelWins(IEnumerable<SelWin> selWins)
    {
        foreach (var selWin in selWins)
        {
            _repository.Insert(selWin);
        }
    }

    public IEnumerable<SelWin> GetAll()
    {
        return _repository.Load();
    }
}