using ConnectorLib.Datastoring;
using ConnectorLib.Masterdata.Models;

namespace ConnectorLib.Masterdata.Services;

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

    public void StoreSelWinsIfNew(IEnumerable<SelWin> selWins)
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