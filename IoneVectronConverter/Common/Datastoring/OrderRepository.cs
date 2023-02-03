using Dapper;
using IoneVectronConverter.Common.Models;
using Microsoft.Data.Sqlite;


namespace IoneVectronConverter.Common.Datastoring;

public class OrderRepository : IRepository<Order>
{
 
    public IQueryable<Order> Load()
    {
        var con = loadConnectionstring();
        
        var sql = 
            $@"select
            Id,
            IoneRefId,
            IoneId,
            Status,
            OrderTotal,
            ReceiptTotal,
            ReceiptUUid,
            ReceiptMainNo,
            Message,
            VposErrorNumber,
            datetime(OrderDate),
            IsCanceldOnPos
            from ione_order";
        
        
        using (var connection = new SqliteConnection(con))
        {
            var result = connection.Query<Order>(sql).AsQueryable();
            return result;
        }
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

    private string loadConnectionstring()
    {
        Console.Write(Directory.GetCurrentDirectory());
        
        string documentsPath = @"..\Resources";
        var stringBuilder = new SqliteConnectionStringBuilder
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            DataSource = Path.Combine( documentsPath, "IoneVectron.sqlite")
        };
        
        Console.Write(stringBuilder.ToString());

        return stringBuilder.ToString();
    }
    
    
}