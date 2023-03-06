using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Common.Masterdata.Services;

public interface IPluService
{
    void StorePlus(IEnumerable<PLU> plus);
    IQueryable<PLU> GetAll();
}