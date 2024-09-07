using EquityServer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EquityServer.Controllers;


public class TradesController : BaseController
{
    private ILogger<TradesController> _logger;

    public TradesController(ILogger<TradesController> logger)
    {
        _logger = logger;
    }

    [HttpPut("")]
    public async Task<IActionResult> Put(TradeEventRequest request)
    {
        _logger.LogWarning("TradeEventRequest: {request}", System.Text.Json.JsonSerializer.Serialize(request));
        return Ok();
    }
}
