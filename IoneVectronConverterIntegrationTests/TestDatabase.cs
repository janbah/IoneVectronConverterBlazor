using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace IoneVectronConverterTests;

public class TestDatabase
{ 
    
    public void ResetDatabase()
    {
        var configuration = GetConfiguration();
        var connectionString = configuration.GetConnectionString("Default");

        string sql = 
            @"drop table tax;
            drop table sel_win_plu_name;
            drop table select_win;
            drop table sel_win;
            drop table price_data;
            drop table plu;
            drop table ione_order;



            create table ione_order
            (
                Id              integer not null
                    primary key autoincrement,
                IoneRefId       integer,
                IoneId          varchar(20),
                Status          integer,
                OrderTotal      DECIMAL(5, 2),
                ReceiptTotal    decimal(5, 2),
                ReceiptUUid     varchar(20),
                ReceiptMainNo   integer,
                Message         varchar(20),
                VposErrorNumber integer,
                OrderDate       text,
                IsCanceledOnPos bit
            );

            create table plu
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

            create table sel_win
            (
                id               integer
                    constraint sel_win_pk
                        primary key,
                selectCompulsion integer,
                selectCount      integer,
                name             text,
                number           integer,
                zeroPriceAllowed integer
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

            create table sel_win_plu_name
            (
                id            integer
                    constraint sel_win_plu_name_pk
                        primary key,
                name          integer,
                select_win_id integer
                    constraint sel_win_plu_name_select_win_id_fk
                        references select_win
                        on update cascade on delete cascade
            );

            create table tax
            (
                id    integer not null
                    constraint tax_pk
                        primary key autoincrement,
                taxNo integer,
                rate  text    not null,
                name  text
            );

            ";

        using (var con = new SqliteConnection(connectionString))
        {
            con.Execute(sql);
        }
    }
            
    public IConfigurationRoot GetConfiguration()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(),"../../../../IoneVectronConverterUnitTests/Resources");
        Console.WriteLine(path);
        return new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();
    }

}