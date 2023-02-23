using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Ione.Services;

public interface IPluService
{
    void StorePluIfNew(IEnumerable<PLU> plus);
    IQueryable<PLU> GetAll();
}