using Dapper;
using Dapper.Contrib.Extensions;
using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Models;
using Microsoft.Data.Sqlite;

namespace IoneVectronConverter.Common.Masterdata.Repositories;

public class TaxRepository : IRepository<Tax>
{
    private readonly string _connectionString;
    public TaxRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public IQueryable<Tax> Load()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var sql = @"select * from tax";
            return connection.Query<Tax>(sql).AsQueryable();
        }
    }

    public long Insert(Tax entity)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            return connection.Insert(entity);
        }
    }

    public void Update(Tax entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}