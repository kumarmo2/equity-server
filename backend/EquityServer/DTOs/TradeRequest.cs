
using EquityServer.Models;

namespace EquityServer.DTOs;




public class TradeEventRequest
{
    public int TradeId { get; set; }
    public string SecurityCode { get; set; }
    public decimal Quantity { get; set; }
    public TradeOperation TradeOperation { get; set; }
    public TradeType TradeType { get; set; }
}
