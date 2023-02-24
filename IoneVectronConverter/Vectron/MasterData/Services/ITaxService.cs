using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Ione.Services;

public interface ITaxService
{
    void StoreTaxesIfNew(IEnumerable<Tax> taxes);
    IEnumerable<Tax> GetAll();
}