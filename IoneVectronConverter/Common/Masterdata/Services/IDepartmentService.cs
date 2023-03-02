namespace IoneVectronConverter.Common.Masterdata.Services;

public interface IDepartmentService
{
    void StoreDepartmentsIfNew(IEnumerable<Department> departments);
}