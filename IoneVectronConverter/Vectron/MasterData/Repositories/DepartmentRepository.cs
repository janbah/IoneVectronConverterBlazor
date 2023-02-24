using IoneVectronConverter.Ione.Datastoring;
using IoneVectronConverter.Vectron.MasterData;
using Department = IoneVectronConverter.Ione.Models.Department;

namespace IoneVectronConverter.Ione.Services;

public class DepartmentRepository : IRepository<Department>
{
    public IQueryable<Department> Load()
    {
        throw new NotImplementedException();
    }

    public long Insert(Department entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Department entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}