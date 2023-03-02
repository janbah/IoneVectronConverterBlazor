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

    public void StorePluIfNew(IEnumerable<PLU> plus)
    {
        foreach (var plu in plus)
        {
            if (pluExistsAlready(plu))
            {
                break;
            }
            _repository.Insert(plu);
        }
    }

    private bool pluExistsAlready(PLU plu)
    {
        var result = GetAll().Any(p => p.PLUno == plu.PLUno);
        return result;
    }

    public IQueryable<PLU> GetAll()
    {
        return _repository.Load();
    }
}