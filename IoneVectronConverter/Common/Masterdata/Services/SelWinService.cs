using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Masterdata.Repositories;
using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Common.Masterdata.Services;

public class SelWinService : ISelWinService
{
    private readonly IRepository<SelWin> _repository;
    public SelWinService(SelWinRepository selWinRepository)
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