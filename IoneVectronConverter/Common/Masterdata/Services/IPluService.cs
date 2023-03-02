using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Common.Masterdata.Services;

public interface IPluService
{
    void StorePluIfNew(IEnumerable<PLU> plus);
    IQueryable<PLU> GetAll();
}