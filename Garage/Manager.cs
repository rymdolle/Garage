using Garage.UserInterface;

namespace Garage;

public class Manager
{
    private IUserInterface _ui;
    private IHandler _handler;
    public Manager(IUserInterface ui, IHandler handler)
    {
        _ui = ui;
        _handler = handler;
    }

    internal void Run()
    {
        _ui.WriteLine("Garage Manager is running. Press Enter to stop.");
        _ui.ReadLine();
        _ui.WriteLine("Garage Manager has stopped.");
    }
}
