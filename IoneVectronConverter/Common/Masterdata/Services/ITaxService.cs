using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Common.Masterdata.Services;

public interface ITaxService
{
    void StoreTaxesIfNew(IEnumerable<Tax> taxes);
    IEnumerable<Tax> GetAll();
}