using EquityServer.Models;

namespace EquityServer.DataAccess;


public interface ITransactionsDao
{
    Task<List<Transaction>> GetAllTransactionsBySecurityCode(string securityCode);
    Task Create(Transaction transaction);

}
