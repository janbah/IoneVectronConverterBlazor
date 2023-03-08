using ConnectorLib.Vectron.Masterdata.Models;

namespace ConnectorLib.Vectron.Masterdata.Services;

public interface ITaxService
{
    void StoreTaxes(IEnumerable<Tax> taxes);
    IEnumerable<Tax> GetAll();
}