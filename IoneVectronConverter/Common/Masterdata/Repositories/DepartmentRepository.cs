using IoneVectronConverter.Common.Datastoring;

namespace IoneVectronConverter.Common.Masterdata.Repositories;

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

    public void Clear()
    {
        throw new NotImplementedException();
    }
}