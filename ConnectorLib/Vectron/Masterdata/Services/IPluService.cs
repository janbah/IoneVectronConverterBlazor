using ConnectorLib.Vectron.Masterdata.Models;

namespace ConnectorLib.Vectron.Masterdata.Services;

public interface IPluService
{
    void StorePlus(IEnumerable<PLU> plus);
    IQueryable<PLU> GetAll();
}