using IoneVectronConverter.Ione.Datastoring;
using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Ione.Services;

public class TaxRepository : IRepository<Tax>
{
    public IQueryable<Tax> Load()
    {
        throw new NotImplementedException();
    }

    public long Insert(Tax entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Tax entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}