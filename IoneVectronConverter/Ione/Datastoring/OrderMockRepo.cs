using IoneVectronConverter.Ione.Models;

namespace IoneVectronConverter.Ione.Datastoring;

public class OrderMockRepo : IRepository<Order>
{
    public IQueryable<Order> Load()
    {
        throw new NotImplementedException();
    }

    public void Insert(Order entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Order entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}