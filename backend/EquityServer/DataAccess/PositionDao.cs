using CommonLibs.Database;
using EquityServer.Models;
using Dapper;

namespace EquityServer.DataAccess;


public class PositionDao : IPositionDao
{
    private readonly IDbConnectionFactory _dbFactory;

    public PositionDao(IDbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public async Task<List<Position>> GetAll()
    {
        var query = @"select * from exchange.positions";


        using (var con = _dbFactory.GetDbConnection())
        {
            return (await con.QueryAsync<Position>(query)).ToList();
        }
    }

    public async Task Create(Position position)
    {
        var query = @"insert into exchange.positions(securitycode, quantity) values (@securitycode, @quantity) on conflict(securitycode) do update set quantity = @quantity;";

        var parameters = new { securitycode = position.SecurityCode, quantity = position.Quantity };

        using (var con = _dbFactory.GetDbConnection())
        {
            await con.ExecuteAsync(query, parameters);
        }
    }
}
