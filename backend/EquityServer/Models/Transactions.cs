namespace EquityServer.Models;


public class Transaction
{
    public int Id { get; set; }
    public int? Version { get; set; }
    public int TradeId { get; set; }
    public string SecurityCode { get; set; }
    public decimal Quantity { get; set; }
    public TradeOperation TradeOperation { get; set; }
    public TradeType TradeType { get; set; }
}
