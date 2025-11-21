using Garage.UserInterface;

namespace Garage;

internal class Program
{
    static void Main(string[] args)
    {
        IHandler handler = new GarageHandler();
        IUserInterface ui = new ConsoleUserInterface();
        Manager manager = new(ui, handler);
        manager.Run();
    }
}
