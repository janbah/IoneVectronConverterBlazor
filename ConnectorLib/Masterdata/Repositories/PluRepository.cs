using ConnectorLib.Datastoring;
using ConnectorLib.Masterdata.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using IoneVectronConverter.Vectron.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace ConnectorLib.Masterdata.Repositories;

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
        //var plus = new List<PLU>();
        
        var connectionString = _configuration.GetConnectionString("Default");

        using (var connection = new SqliteConnection(connectionString))
        {

            var sql = @"select * from plu";

            var plus = connection.Query<PLU>(sql);

            foreach (var plu in plus)
            {
                plu.Prices = getPricesForPlu(plu.Id, connection);
                plu.SelectWin = getSelWinsForPlu(plu.Id, connection);
            }
            
            return plus.AsQueryable();
        }
    }

    private PriceData[] getPricesForPlu(int id,SqliteConnection connection )
    {
        var sql = @"select Level, Price from price_data where plu_id = @p1 ";
        var priceList = connection.Query<PriceData>(sql, new{@p1=id});
        return priceList.ToArray();
    }
    
    private int[] getSelWinsForPlu(int id,SqliteConnection connection )
    {
        var sql = @"select value from select_win where plu_id = @p1 ";
        var selectWins = connection.Query<int>(sql, new{@p1=id});
        return selectWins.ToArray();
    }
    
    

    public long Insert(PLU entity)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        using (var connection = new SqliteConnection(connectionString))
        {
            var id = connection.Insert<PLU>(entity);

            insertPrices(entity, id, connection);
            insertSelWins(entity, id, connection);
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

    public void Clear()
    {
        var connectionString = _configuration.GetConnectionString("Default");

        string sql = 
            @"
            drop table if exists select_win;
            drop table if exists price_data;
            drop table if exists plu;
            
            create table  plu
            (
                id           integer
                    constraint plu_pk
                        primary key,
                PLUno        integer,
                Name1        text,
                Name2        text,
                Name3        text,
                SaleAllowed  integer,
                TaxNo        integer,
                DepartmentNo integer,
                Attributes   text,
                MainGroupA   integer,
                MainGroupB   integer,
                IsForWebShop integer
            );

            create table price_data
            (
                id     integer
                    constraint price_data_pk
                        primary key,
                plu_id integer
                    constraint price_data_plu_id_fk
                        references plu,
                Level  integer,
                Price  text
            );

     

            create table select_win
            (
                id     integer
                    constraint select_win_pk
                        primary key,
                value  integer,
                plu_id integer
                    constraint select_win_plu_id_fk
                        references plu
            );


            ";

        using (var con = new SqliteConnection(connectionString))
        {
            con.Execute(sql);
        }
    }

    private void insertSelWins(PLU entity, long id, SqliteConnection connection)
    {
        foreach (var selWin in entity.SelectWin)
        {
            var sql = "INSERT INTO select_win (plu_id, value) VALUES(@id, @selWin)";
            var selWinData = new
            {
                id,
                selWin,
            };

            connection.Execute(sql, selWinData);
        }
    }

    private static void insertPrices(PLU entity, long id, SqliteConnection connection)
    {
        foreach (var price in entity.Prices)
        {
            var sql = "INSERT INTO price_data (plu_id, Level, Price) VALUES(@id,@Price, @Level)";
            var priceData = new
            {
                id,
                price.Price,
                price.Level
            };

            connection.Execute(sql, priceData);
        }
    }
}