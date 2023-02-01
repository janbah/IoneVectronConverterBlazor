using IoneVectronConverter.Common.Models;
using Microsoft.Data.Sqlite;


namespace IoneVectronConverter.Common.Datastoring;

public class OrderRepository : IRepository<Order>
{
    public IQueryable<Order> Load()
    {
        throw new NotImplementedException();
    }

    public void Insert(Order entity)
    {
        using (var connection = new SqliteConnection(LoadConnectionstring()))
        {
            connection.Open();
            
        }
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

    private static string LoadConnectionstring()
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var stringBuilder = new SqliteConnectionStringBuilder
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            DataSource = documentsPath + "\\orders.sqlite"
        };
        return stringBuilder.ToString();
    }
    
    
}