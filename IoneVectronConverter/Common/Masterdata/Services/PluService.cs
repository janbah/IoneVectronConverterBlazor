using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Masterdata.Repositories;
using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Common.Masterdata.Services;

public class PluService : IPluService
{
    private readonly IRepository<PLU> _repository;

    public PluService(IRepository<PLU> repository)
    {
        _repository = repository;
    } 
    public PluService()
    {
    }

    public void StorePlus(IEnumerable<PLU> plus)
    {
        _repository.Clear();
        foreach (var plu in plus)
        {
            _repository.Insert(plu);
        }
    }

    private bool pluExistsAlready(PLU plu)
    {
        var result = GetAll().Any(p => p.PLUno == plu.PLUno);
        return result;
    }

    public void ClearTable()
    {
        _repository.Clear();
    }

    public IQueryable<PLU> GetAll()
    {
        return _repository.Load();
    }
}