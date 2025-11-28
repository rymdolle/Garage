using Garage.UserInterface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Garage;

internal class Program
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<IConfiguration>(config);
                services.AddSingleton<IUserInterface, ConsoleUserInterface>();
                services.AddSingleton<IHandler, GarageHandler>();
                services.AddSingleton<Manager>();
            })
            .Build();
        host.Services.GetRequiredService<Manager>().Run();
    }
}
