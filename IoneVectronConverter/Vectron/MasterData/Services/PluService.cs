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
        throw new NotImplementedException();

    }

    public IQueryable<PLU> GetAll()
    {
        return _repository.Load();
    }
}