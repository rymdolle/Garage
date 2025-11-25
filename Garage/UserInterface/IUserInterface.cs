namespace Garage.UserInterface;

public interface IUserInterface
{
    public string ReadLine();
    public void WriteLine(string message);
    public void Clear();
    public int SelectMenuOption(string prompt, (int, string)[] options, string errorMessage);
    public void WriteError(string message);
}
