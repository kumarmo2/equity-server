
using EquityServer.DataAccess;
using EquityServer.DTOs;
using EquityServer.Models;

namespace EquityServer.Business;

public class TradesLogic : ITradesLogic
{
    private readonly ITransactionsDao _transactionsDao;
    private readonly ILogger<TradesLogic> _logger;
    private readonly IPositionDao _positionDao;

    public TradesLogic(ITransactionsDao transactionsDao, ILogger<TradesLogic> logger, IPositionDao positionDao)
    {
        _transactionsDao = transactionsDao;
        _logger = logger;
        _positionDao = positionDao;
    }

    public async Task<List<Position>> GetAllPositions()
    {
        return await _positionDao.GetAll();
    }


    public async Task HandleTradeEventRequest(TradeEventRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var securityCode = request.SecurityCode;
        var transactions = await _transactionsDao.GetAllTransactionsBySecurityCode(securityCode);
        _logger.LogInformation("... reached here, count: {count}", transactions.Count);


        var newTransaction = new Transaction
        {
            SecurityCode = securityCode,
            TradeType = request.TradeType,
            TradeId = request.TradeId,
            Quantity = request.Quantity,
            TradeOperation = request.TradeOperation,
            Version = 1, // needs to be calculated
        };
        //
        // TODO: recalculate versions of the "trade events";

        transactions.Add(newTransaction);

        var transactionByTradeId = transactions.GroupBy(tr => tr.TradeId);


        var quantity = transactions.GroupBy(tr => tr.TradeId)
        .Select(tradeGroup =>
        {

            var tradeId = tradeGroup.Key;
            _logger.LogInformation("tradeId: {tradeId}", tradeId);

            var cancelledTrade = tradeGroup.FirstOrDefault(tr => tr.TradeOperation == TradeOperation.Cancel);
            if (cancelledTrade is not null)
            {
                return 0;
            }
            var insertedTrade = tradeGroup.FirstOrDefault(tr => tr.TradeOperation == TradeOperation.Insert);
            if (insertedTrade is null)
            {
                // we have decided that if "inserted" trade is not found, we will set the quantity to 0.
                return 0;
            }
            Console.WriteLine($"inserted record found, {insertedTrade.Quantity}");

            var quantity = insertedTrade.TradeType == TradeType.Buy ? insertedTrade.Quantity : -insertedTrade.Quantity;
            var updatedRecords = tradeGroup.Where(tr => tr.TradeOperation == TradeOperation.Update);
            var updatedRecordsCount = updatedRecords.ToList();
            Console.WriteLine($"updated recourd count: {updatedRecordsCount.Count}");

            foreach (var updatedTrade in tradeGroup.Where(tr => tr.TradeOperation == TradeOperation.Update))
            {
                quantity = updatedTrade.TradeType == TradeType.Buy ? updatedTrade.Quantity : updatedTrade.Quantity;
                Console.WriteLine($"....UPDATED record found, {updatedTrade.Quantity}, new quantity: {quantity}");
            }
            return quantity;

        }).Sum();
        Console.Write($"..... here, quantity: {quantity}");


        var position = new Position
        {
            SecurityCode = request.SecurityCode,
            Quantity = quantity,
        };
        await _positionDao.Create(position);
        await _transactionsDao.Create(newTransaction);
    }



}
