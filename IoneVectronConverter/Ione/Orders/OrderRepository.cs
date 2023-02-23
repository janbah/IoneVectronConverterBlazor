using Dapper;
using Dapper.Contrib.Extensions;
using IoneVectronConverter.Ione.Models;
using Microsoft.Data.Sqlite;

namespace IoneVectronConverter.Ione.Datastoring;

public class OrderRepository : IRepository<Order>
{

    private readonly IConfiguration _configuration;

    public OrderRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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
            IsCanceledOnPos
            from ione_order";
        
        
        using (var connection = new SqliteConnection(con))
        {
            var result = connection.Query<Order>(sql).AsQueryable();
            return result;
        }
    }

    public long Insert(Order entity)
    {
        var con = loadConnectionstring();
        //
        // var sql = @"insert into ione_order
        //                 (
        //                 IoneRefId,
        //                 IoneId,
        //                 Status,
        //                 OrderTotal,
        //                 ReceiptTotal,
        //                 ReceiptUUid,
        //                 ReceiptMainNo,
        //                 Message,
        //                 VposErrorNumber,
        //                 OrderDate,
        //                 IsCanceldOnPos
        //                 )
        //             values
        //                 (
        //                     
        //                 )";
      

        using (var connection = new SqliteConnection(con))
        {
            var id = connection.Insert<Order>(entity);
            return id;
        }
    }

    public void Update(Order entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        var con = loadConnectionstring();

        using (var connection = new SqliteConnection(con))
        {
            var sql = @"delete from ione_order where id = @id";
            connection.Execute(sql, new {id});
        }
    }

    private string loadConnectionstring()
    {
        return _configuration.GetConnectionString("Default");
    }
    
    
}