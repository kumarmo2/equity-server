using EquityServer.Business;
using EquityServer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EquityServer.Controllers;


public class TradesController : BaseController
{
    private readonly ILogger<TradesController> _logger;
    private readonly ITradesLogic _tradesLogic;

    public TradesController(ILogger<TradesController> logger, ITradesLogic tradesLogic)
    {
        _logger = logger;
        _tradesLogic = tradesLogic;
    }

    [HttpPut("")]
    public async Task<IActionResult> Put(TradeEventRequest request)
    {
        _logger.LogWarning("TradeEventRequest: {request}", System.Text.Json.JsonSerializer.Serialize(request));
        await _tradesLogic.HandleTradeEventRequest(request);
        return Ok();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _tradesLogic.GetAllPositions());
    }
}
