namespace Garage.UserInterface;

public class ConsoleUserInterface : IUserInterface
{
    public string ReadLine()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    public void Clear()
    {
        Console.Clear();
    }

    public int SelectMenuOption(string prompt, (int, string)[] options, string errorMessage)
    {
        // Display menu
        Console.WriteLine(prompt);
        foreach (var (index, item) in options)
            Console.WriteLine($"{index}. {item}");

        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                foreach (var (index, item) in options)
                {
                    if (index == choice)
                        return choice;
                }
            }

            WriteError(errorMessage);
        }
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
