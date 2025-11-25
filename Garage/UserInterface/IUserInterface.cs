namespace Garage.UserInterface;

public interface IUserInterface
{
    public string ReadLine();
    public void WriteLine(string message);
    public void Write(string message);
    public void Clear();
    public int SelectMenuOption(string prompt, Menu options, string errorMessage);
    public void WriteError(string message);
    int ReadInt(string prompt, Predicate<int> constraint, string errorMessage);
    string ReadString(string prompt, Predicate<string> constraint, string errorMessage);
}
