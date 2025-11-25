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

    public void Run()
    {
        Menu mainMenu = new Menu("Main menu");
        Menu? menu = new Menu("Main Menu", [
            new Menu("List all vehicles", ListAllVehicles),
            new Menu("Park vehicle", AddVehicle),
            new Menu("Remove vehicle", RemoveVehicle),
            new Menu("Search vehicle", SearchVehicle),
            new Menu("Mock vehicles", CreateMockVehicles),
            ]
        );
        while (menu != null)
        {
            menu = menu.Navigate(_ui);
        }
    }
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
        try
        {
            Vehicle vehicle = choice switch
            {
                1 => new Car(regnr, Car.BodyType.Undefined),
                2 => new Motorcycle(regnr, maxLeanAngle: 24),
                3 => new Bus(regnr, seatingCapacity: 12),
                4 => new Airplane(regnr, wingspan: 10),
                5 => new Boat(regnr, displacement: 40),
                _ => throw new InvalidOperationException("Invalid vehicle type selected.")
            };
            _handler.AddVehicle(vehicle);
            ui.WriteLine($"Vehicle {vehicle} parked in the garage.");
        }
        catch (Exception ex)
        {
            ui.WriteError($"Error parking vehicle: {ex.Message}");
        }
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
        if (_handler.RemoveVehicle(vehicle))
        {
            ui.WriteLine($"Vehicle {vehicle.RegistrationNumber} removed from the garage.");
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
        _handler.AddVehicle(new Car("ABC123", Car.BodyType.Hatchback)
        {
            Color = "Red"
        });
        _handler.AddVehicle(new Motorcycle("XYZ789", maxLeanAngle: 24)
        {
            Color = "Black"
        });
        _handler.AddVehicle(new Bus("BUS456", seatingCapacity: 56)
        {
            Color = "Blue"
        });
        _handler.AddVehicle(new Bus("BUS457", seatingCapacity: 56)
        {
            Color = "Green"
        });
        _handler.AddVehicle(new Bus("BUS458", seatingCapacity: 40)
        {
            Color = "Yellow"
        });
        _handler.AddVehicle(new Bus("BUS459", seatingCapacity: 32)
        {
            Color = "Yellow"
        });
        _handler.AddVehicle(new Bus("BUS451", seatingCapacity: 22)
        {
            Color = "Red"
        });
        _handler.AddVehicle(new Bus("BUS452", seatingCapacity: 22)
        {
            Color = "Red"
        });
        _handler.AddVehicle(new Bus("BUS453", seatingCapacity: 18)
        {
            Color = "Red"
        });
        _handler.AddVehicle(new Bus("BUS454", seatingCapacity: 92)
        {
            Color = "White"
        });
        _ui.WriteLine("Mock vehicles added to the garage.");
    }
}
