using ConnectorLib.Vectron.Masterdata.Models;

namespace ConnectorLib.Vectron.Masterdata.Services;

public interface IDepartmentService
{
    void StoreDepartments(IEnumerable<Department> departments);
}