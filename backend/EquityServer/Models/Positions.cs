namespace EquityServer.Models;


public class Position
{
    public int Id { get; set; }
    public string SecurityCode { get; set; }
    public decimal Quantity { get; set; }
}
