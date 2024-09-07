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





        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
