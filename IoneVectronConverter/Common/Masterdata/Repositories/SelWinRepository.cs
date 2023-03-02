using Dapper;
using Dapper.Contrib.Extensions;
using IoneVectronConverter.Common.Datastoring;
using IoneVectronConverter.Common.Models;
using Microsoft.Data.Sqlite;

namespace IoneVectronConverter.Common.Masterdata.Repositories;

public class SelWinRepository : IRepository<SelWin>
{
    private readonly string _connectionString;
    public SelWinRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public IQueryable<SelWin> Load()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var sql = @"select * from sel_win";
            var selWins  = connection.Query<SelWin>(sql).AsQueryable();

            foreach (var selWin in selWins)
            {
                selWin.PLUs = getPlusForSelwin(selWin.Id, connection);
            }

            return selWins;
        }
    }

    private string[] getPlusForSelwin(int selWinId, SqliteConnection connection)
    {
        var sql = "select name from sel_win_plu_name";
        var result = connection.Query<string>(sql);
        return result.ToArray();
    }

    public long Insert(SelWin entity)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
           var id =connection.Insert(entity);
           insertSelWins(entity, id, connection);
           return id;
        }
    }

    public void Update(SelWin entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    
    private void insertSelWins(SelWin entity, long id, SqliteConnection connection)
    {
        foreach (var plu in entity.PLUs)
        {
            var sql = "INSERT INTO sel_win_plu_name (select_win_id, name) VALUES(@id, @plu)";
            var selWinData = new
            {
                id,
                plu,
            };

            connection.Execute(sql, selWinData);
        }
    }

}