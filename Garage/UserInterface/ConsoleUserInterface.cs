namespace Garage.UserInterface;

public class ConsoleUserInterface : IUserInterface
{
    public string ReadLine()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}
