using Garage.UserInterface;
using Garage.Vehicles;

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
        Menu? menu = new ConsoleMenu("Main Menu", [
            new ConsoleMenu("List all vehicles", ListAllVehicles),
            new ConsoleMenu("Park vehicle", AddVehicle),
            new ConsoleMenu("Remove vehicle", RemoveVehicle),
            new ConsoleMenu("Search vehicle", SearchVehicle),
            new ConsoleMenu("Mock vehicles", CreateMockVehicles),
            ]
        );
        do
        {
            try
            {
                menu = menu.Navigate(_ui);
            }
            catch (Exception ex)
            {
                _ui.WriteError($"An error occurred: {ex.Message}");
            }
        } while (menu != null);
    }

    private void AddVehicle(IUserInterface ui)
    {
        ui.WriteLine("Select vehicle type to park:");
        var options = new (int, string)[]
        {
            (1, "Car"),
            (2, "Motorcycle"),
            (3, "Bus"),
            (4, "Airplane"),
            (5, "Boat"),
            (0, "Cancel"),
        };
        int choice = ui.SelectMenuOption("Vehicle Types:", options, "Invalid selection. Please try again.");
        if (choice == 0)
            return;
        ui.WriteLine("Enter registration number:");
        string regnr = ui.ReadLine();
        Vehicle vehicle = choice switch
        {
            1 => new Car(regnr),
            2 => new Motorcycle(regnr),
            3 => new Bus(regnr),
            4 => new Airplane(regnr),
            5 => new Boat(regnr),
            _ => throw new InvalidOperationException("Invalid vehicle type selected.")
        };
        try
        {
            _handler.AddVehicle(vehicle);
        }
        catch (InvalidOperationException ex)
        {
            ui.WriteError(ex.Message);
            return;
        }
        ui.WriteLine($"Vehicle {vehicle} parked in the garage.");
    }

    private void RemoveVehicle(IUserInterface ui)
    {
        ui.WriteLine("Enter registration number of the vehicle to remove:");
        string regnr = ui.ReadLine();
        var vehicle = _handler.GetVehicleByRegNr(regnr);
        if (vehicle == null)
        {
            ui.WriteError($"No vehicle found with registration number {regnr}.");
            return;
        }
        bool removed = _handler.RemoveVehicle(vehicle);
        if (removed)
        {
            ui.WriteLine($"Vehicle {vehicle} removed from the garage.");
        }
        else
        {
            ui.WriteError($"Failed to remove vehicle {vehicle}.");
        }
    }

    private void SearchVehicle(IUserInterface ui)
    {
        ui.WriteLine("Enter search query (e.g., color=red type=car regnr=ABC123):");
        string query = ui.ReadLine();
        var results = _handler.SearchVehicle(query);
        if (!results.Any())
        {
            ui.WriteLine("No vehicles found matching the query.");
            return;
        }

        ui.WriteLine("Search Results:");
        foreach (var vehicle in results)
        {
            ui.WriteLine(vehicle.ToString());
        }
    }

    private void ListAllVehicles(IUserInterface ui)
    {
        var vehicles = _handler.GetAllVehicles();
        if (!vehicles.Any())
        {
            _ui.WriteLine("No vehicles parked in the garage.");
            return;
        }
        foreach (var vehicle in vehicles)
        {
            _ui.WriteLine(vehicle.ToString());
        }
    }

    private void CreateMockVehicles(IUserInterface ui)
    {
        _handler.AddVehicle(new Car("ABC123"));
        _handler.AddVehicle(new Motorcycle("XYZ789"));
        _handler.AddVehicle(new Bus("BUS456"));
        _ui.WriteLine("Mock vehicles added to the garage.");
    }
}
