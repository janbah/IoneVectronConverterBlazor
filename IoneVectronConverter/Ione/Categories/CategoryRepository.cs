using Dapper;
using Dapper.Contrib.Extensions;
using IoneVectronConverter.Common.Datastoring;
using Microsoft.Data.Sqlite;

namespace IoneVectronConverter.Ione.Categories;

public class CategoryRepository : IRepository<Category>
{
    private readonly string _connectionString;
    public CategoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    public IQueryable<Category> Load()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var sql = "select * from category";
            return connection.Query<Category>(sql).AsQueryable();
        }
    }

    public long Insert(Category entity)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            return connection.Insert(entity);
        }
    }

    public void Update(Category entity)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Update(entity);
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Delete<Category>(new Category(){Id = id});
        }
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }
}