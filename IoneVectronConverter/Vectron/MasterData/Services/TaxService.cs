using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Ione.Services;

public class TaxService : ITaxService
{
    private readonly TaxRepository _taxRepository;
    public TaxService(TaxRepository taxRepository)
    {
        _taxRepository = taxRepository;
    }

    public void StoreTaxesIfNew(IEnumerable<Tax> taxes)
    {
        foreach (var tax in taxes)
        {
            _taxRepository.Insert(tax);
        }
    }

    public IEnumerable<Tax> GetAll()
    {
        return _taxRepository.Load();
    }
}