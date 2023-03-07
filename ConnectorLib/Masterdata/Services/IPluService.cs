using ConnectorLib.Masterdata.Models;

namespace ConnectorLib.Masterdata.Services;

public interface IPluService
{
    void StorePlus(IEnumerable<PLU> plus);
    IQueryable<PLU> GetAll();
}