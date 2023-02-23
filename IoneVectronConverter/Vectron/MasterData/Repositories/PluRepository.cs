using Dapper.Contrib.Extensions;
using IoneVectronConverter.Ione.Datastoring;
using IoneVectronConverter.Vectron.MasterData;
using Microsoft.Data.Sqlite;

namespace IoneVectronConverter.Ione.Services;

public class PluRepository : IRepository<PLU>
{
    private readonly IConfiguration _configuration;

    public PluRepository(IConfiguration configuration)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        _configuration = configuration;
    }

    public IQueryable<PLU> Load()
    {
        throw new NotImplementedException();
    }

    public long Insert(PLU entity)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        using (var connection = new SqliteConnection(connectionString))
        {
            var id = connection.Insert<PLU>(entity);
            return id;
        }
    }

    public void Update(PLU entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}