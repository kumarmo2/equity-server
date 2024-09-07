using EquityServer.DTOs;
using EquityServer.Models;

namespace EquityServer.Business;

public interface ITradesLogic
{
    Task HandleTradeEventRequest(TradeEventRequest request);
    Task<List<Position>> GetAllPositions();
}
