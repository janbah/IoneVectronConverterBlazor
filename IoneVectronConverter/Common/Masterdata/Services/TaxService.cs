using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Masterdata.Repositories;
using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Common.Masterdata.Services;

public class TaxService : ITaxService
{
    private readonly IRepository<Tax> _taxRepository;
    public TaxService(IRepository<Tax> taxRepository)
    {
        _taxRepository = taxRepository;
    }
    public TaxService()
    {
    }

    public void StoreTaxesIfNew(IEnumerable<Tax> taxes)
    {
        foreach (var tax in taxes)
        {
            if (taxExistsAlready(tax))
            {
                break;
            }
            _taxRepository.Insert(tax);
        }
    }

    private bool taxExistsAlready(Tax tax)
    {
        var result = GetAll().Any(t => t.TaxNo == tax.TaxNo);
        return result;
    }

    public IEnumerable<Tax> GetAll()
    {
        return _taxRepository.Load();
    }
}