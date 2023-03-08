using ConnectorLib.Common.Datastoring;
using ConnectorLib.Vectron.Masterdata.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace ConnectorLib.Vectron.Masterdata.Repositories;

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

    public void Clear()
    {
        throw new NotImplementedException();
    }
}