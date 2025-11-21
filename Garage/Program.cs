using Garage.UserInterface;

namespace Garage;

internal class Program
{
    static void Main(string[] args)
    {
        Handler handler = new();
        IUserInterface ui = new ConsoleUserInterface();
        Manager manager = new(ui, handler);
        manager.Run();
    }
}
