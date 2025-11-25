namespace Garage.UserInterface;

public class Menu(string title, Action<IUserInterface>? action = null)
{
    public string Title { get; } = title;
    public Menu? Parent { get; protected set; }
    public List<Menu> Children { get; } = [];
    public Action<IUserInterface>? Action { get; } = action;

    public void AddChild(Menu child)
    {
        child.Parent = this;
        Children.Add(child);
    }

    public Menu(string title, params Menu[] entries)
    : this(title)
    {
        foreach (var entry in entries)
            AddMenu(entry);
    }

    public Menu AddMenu(Menu entry)
    {
        entry.Parent = this;
        Children.Add(entry);
        return this;
    }

    public Menu? Navigate(IUserInterface ui)
    {
        ui.Clear();

        PrintBreadcrums(ui);

        Action?.Invoke(ui);

        // Enumerate options and add back/exit option
        var options = Children.Select((m, index) => (index + 1, m.Title)).ToList();
        options.Add((0, Parent != null ? "Back" : "Exit"));

        int choice = ui.SelectMenuOption("Select option:", options.ToArray(), "Invalid selection. Please try again.");
        if (Children.Count == 0 || choice == 0)
            return Parent;

        return Children[choice - 1];
    }

    private void PrintBreadcrums(IUserInterface ui)
    {
        List<string> breadcrumb = [];
        for (Menu? m = Parent; m != null; m = m.Parent)
            breadcrumb.Insert(0, m.Title);
        breadcrumb.Add(Title);
        ui.WriteLine(string.Join(" > ", breadcrumb));
    }
}
