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

    public int SelectMenuOption(string prompt, Menu options, string errorMessage)
    {
        // Display menu
        Console.WriteLine(prompt);
        for (int i = 0; i < options.Children.Count; i++)
            Console.WriteLine($"{i + 1}. {options.Children[i].Title}");
        string parentText = options.Parent == null ? "Exit" : "Back";
        Console.WriteLine($"0. {parentText}");

        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 0 && choice <= options.Children.Count)
            {
                return choice;
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

    public int ReadInt(string prompt, Predicate<int> constraint, string errorMessage)
    {
        while (true)
        {
            WriteLine(prompt);
            if (int.TryParse(ReadLine(), out int result) && constraint.Invoke(result))
            {
                return result;
            }
            WriteError(errorMessage);
        }
    }

    public string ReadString(string prompt, Predicate<string>? constraint, string? errorMessage)
    {
        while (true)
        {
            WriteLine(prompt);
            string input = ReadLine();
            if (constraint?.Invoke(input) ?? true)
            {
                return input;
            }
            if (errorMessage != null)
                WriteError(errorMessage);
        }
    }
}
