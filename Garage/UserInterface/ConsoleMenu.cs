namespace Garage.UserInterface;

public class ConsoleMenu(string title, Action<IUserInterface>? action = null) : Menu(title, action)
{
    public ConsoleMenu(string title, params ConsoleMenu[] subMenus) : this(title)
    {
        foreach (var menu in subMenus)
            AddMenu(menu);
    }
}
