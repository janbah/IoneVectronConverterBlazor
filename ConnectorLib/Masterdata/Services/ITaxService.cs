using ConnectorLib.Masterdata.Models;

namespace ConnectorLib.Masterdata.Services;

public interface ITaxService
{
    void StoreTaxesIfNew(IEnumerable<Tax> taxes);
    IEnumerable<Tax> GetAll();
}