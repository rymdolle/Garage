namespace Garage.UserInterface;

public class Menu(string title, Action<Menu>? action = null)
{
    public string Title { get; } = title;
    public Menu? Parent { get; set; }
    public List<Menu> Children { get; } = [];
    public Action<Menu>? Action { get; } = action;

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

        Action?.Invoke(this);

        if (Children.Count == 0)
        {
            ui.WriteLine("Press Enter to go back...");
            ui.ReadLine();
            return Parent;
        }
        int choice = ui.SelectMenuOption("Select option:", this, "Invalid selection. Please try again.");
        if (choice == 0)
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
