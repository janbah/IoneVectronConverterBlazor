using IoneVectronConverter.Common;

namespace ConnectorLib.Masterdata.Services;

public interface IDepartmentService
{
    void StoreDepartmentsIfNew(IEnumerable<Department> departments);
}