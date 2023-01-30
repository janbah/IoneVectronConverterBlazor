using Order2VPos.Core.IoneApi.Orders;

namespace DataStoring.Repositories;

public class OrderRepository :IRepository<OrderItem>
{
    public IQueryable<OrderItem> Load()
    {
        throw new NotImplementedException();
    }

    public void Insert(OrderItem entity)
    {
        throw new NotImplementedException();
    }

    public void Update(OrderItem entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}