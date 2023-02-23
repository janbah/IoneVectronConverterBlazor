using IoneVectronConverter.Ione.Models;

namespace IoneVectronConverter.Ione.Services;

public interface IDepartmentService
{
    void StoreDepartmentsIfNew(IEnumerable<Department> departments);
}