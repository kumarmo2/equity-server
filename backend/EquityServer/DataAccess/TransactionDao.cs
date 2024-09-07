
using CommonLibs.Database;
using Dapper;
using EquityServer.Models;

namespace EquityServer.DataAccess;


public class TransactionsDao : ITransactionsDao
{
    private readonly IDbConnectionFactory _dbFactory;

    public TransactionsDao(IDbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task Create(Transaction transaction)
    {
        var query = @"insert into exchange.transactions( tradeid, version , securitycode , quantity , tradeoperationtype , tradetype )
                values (  @tradeid, @version , @securitycode , @quantity , @tradeoperationtype , @tradetype )";
        var parameters = new
        {
            tradeid = transaction.TradeId,
            version = transaction.Version,
            securitycode = transaction.SecurityCode,
            quantity = transaction.Quantity,
            tradeoperationtype = transaction.TradeOperation,
            tradetype = transaction.TradeType
        };
        using (var con = _dbFactory.GetDbConnection())
        {
            await con.ExecuteAsync(query, parameters);
        }

    }

    public async Task<List<Transaction>> GetAllTransactionsBySecurityCode(string securityCode)
    {
        var query = "select  tradeid, version , securitycode , quantity , tradeoperationtype as tradeoperation, tradetype  from exchange.transactions where securityCode = @securitycode";
        var parameters = new { securitycode = securityCode };
        using (var con = _dbFactory.GetDbConnection())
        {
            var transactions = (await con.QueryAsync<Transaction>(query, parameters))?.ToList() ?? new List<Transaction>();
            return transactions;
        }
    }
}
