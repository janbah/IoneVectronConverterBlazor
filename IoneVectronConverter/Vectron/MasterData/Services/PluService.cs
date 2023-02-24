using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Ione.Services;

public class PluService : IPluService
{
    private readonly PluRepository _repository;

    public PluService(PluRepository repository)
    {
        _repository = repository;
    }

    public void StorePluIfNew(IEnumerable<PLU> plus)
    {
        foreach (var plu in plus)
        {
            _repository.Insert(plu);
        }
    }

    public IQueryable<PLU> GetAll()
    {
        return _repository.Load();
    }
}