using EquityServer.Business;
using EquityServer.DataAccess;
using CommonLibs.Database;

namespace EquityServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var config = builder.Configuration;
        services.AddControllers();
        services.AddLogging(options => options.AddJsonConsole());
        services.AddDatabase(config);

        services.AddSingleton<ITradesLogic, TradesLogic>();
        services.AddSingleton<ITransactionsDao, TransactionsDao>();
        services.AddSingleton<IPositionDao, PositionDao>();





        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
