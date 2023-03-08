using ConnectorLib.Common.Datastoring;
using ConnectorLib.Vectron.Masterdata.Models;

namespace ConnectorLib.Vectron.Masterdata.Services;

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

    public void StoreTaxes(IEnumerable<Tax> taxes)
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