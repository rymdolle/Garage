using Garage.UserInterface;
using Garage.Vehicles;

namespace Garage;

public class Manager
{
    private IUserInterface _ui;
    private IHandler _handler;

    private Menu _mainMenu;
    public Manager(IUserInterface ui, IHandler handler)
    {
        _ui = ui;
        _handler = handler;

        _mainMenu = new("Garage", [
            new Menu("List all vehicles", ListAllVehicles),
            new Menu("List vehicle count by type", ListVehicleCountByType),
            new Menu("Park vehicle", [
                new Menu("Car", ParkVehicle<Car>),
                new Menu("Motorcycle", ParkVehicle<Motorcycle>),
                new Menu("Bus", ParkVehicle<Bus>),
                new Menu("Airplane", ParkVehicle<Airplane>),
                new Menu("Boat", ParkVehicle<Boat>),
            ]),
            new Menu("Remove vehicle", RemoveVehicle),
            new Menu("Search vehicle", SearchVehicle),
            ]);
    }

    private void ListVehicleCountByType()
    {
        var vehicleCounts = _handler.VehicleCountByType();
        foreach (var (key, value) in vehicleCounts)
        {
            _ui.WriteLine($"{key}: {value}");
        }

        int total = vehicleCounts.Sum(entry => entry.Value);
        _ui.WriteLine($"Total: {total}");

    }

    public void Run()
    {
        _ui.WriteLine("Welcome to Parkin Garage Application!");
        int capacity = _ui.ReadInt("Choose garage capacity:", c => c > 0, "Capacity has to be more than 0");
        _handler.CreateGarage(capacity);
        _mainMenu.Title += $" ({capacity})";
        if (_ui.ReadString("Do you want to populate with demo vehicles? y/N").Equals("y", StringComparison.CurrentCultureIgnoreCase))
        {
            CreateMockVehicles();
            _ui.ReadString("Press enter to continue...");
        }

        Menu? menu = _mainMenu;
        while (menu != null)
        {
            try
            {
                menu = menu.Navigate(_ui);
            }
            catch (Exception e)
            {
                _ui.WriteError(e.Message);
                _ui.WriteLine("Press enter to return to main menu...");
                _ui.ReadLine();
                menu = _mainMenu;
            }
        }
    }

    private void ParkVehicle<T>() where T : Vehicle
    {
        string regnr = _ui.ReadString("Registration number (empty to abort):", c => _handler.GetVehicleByRegNr(c) == null, "Registration number has to be unique");
        if (regnr == string.Empty)
        {
            _ui.WriteError("Aborted.");
            return;
        }
        Vehicle vehicle = typeof(T) switch
        {
            Type t when t == typeof(Car) =>
                new Car(regnr, _ui.ReadString("Body type:", c => c != string.Empty, "Body type can't be empty")),
            Type t when t == typeof(Motorcycle) =>
                new Motorcycle(regnr, _ui.ReadInt("Max lean angle:", c => c > 0, "Max lean angle has to be an integer greater than 0")),
            Type t when t == typeof(Bus) =>
                new Bus(regnr, _ui.ReadInt("Seat capacity:", c => c > 0, "Capacity has to be an integer greater than 0")),
            Type t when t == typeof(Airplane) =>
                new Airplane(regnr, _ui.ReadInt("Wing span:", c => c > 0, "Wing span has to be an integer greater than 0")),
            Type t when t == typeof(Boat) =>
                new Boat(regnr, _ui.ReadInt("Displacement:", c => c > 0, "Displacement has to be an integer greater than 0")),
            _ =>
                throw new NotImplementedException($"Vehicle type {typeof(T).Name} not implemented in CreateVehicle"),
        };

        vehicle.Color = _ui.ReadString("Color:");

        _ui.WriteLine(vehicle.ToString());
        string confirm = _ui.ReadString("Is this correct? Y/n");
        if (confirm.ToLower().Equals("n"))
        {
            _ui.WriteError("Aborted.");
            return;
        }
        _handler.AddVehicle(vehicle);
        _ui.WriteLine($"{vehicle.GetType().Name} created.");
    }


    private void RemoveVehicle()
    {
        _ui.WriteLine("Enter registration number of the vehicle to remove:");
        string regnr = _ui.ReadLine();
        var vehicle = _handler.GetVehicleByRegNr(regnr);
        if (vehicle == null)
        {
            _ui.WriteError($"No vehicle found with registration number {regnr}.");
            return;
        }
        if (_handler.RemoveVehicle(vehicle))
        {
            _ui.WriteLine($"Vehicle {vehicle.RegistrationNumber} removed from the garage.");
        }
        else
        {
            _ui.WriteError($"Failed to remove vehicle {vehicle}.");
        }
    }

    private void SearchVehicle()
    {
        _ui.WriteLine("Enter search query (e.g., color=red type=car regnr=ABC123):");
        string query = _ui.ReadLine();
        var results = _handler.SearchVehicle(query);
        if (!results.Any())
        {
            _ui.WriteLine("No vehicles found matching the query.");
            return;
        }

        _ui.WriteLine("Search Results:");
        foreach (var vehicle in results)
        {
            _ui.WriteLine(vehicle.ToString());
        }
    }

    private void ListAllVehicles()
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

    private void CreateMockVehicles()
    {
        Vehicle[] vehicles = [
            new Car("ABC123", "Sedan")
            {
                Color = "Red"
            },
            new Motorcycle("XYZ789", maxLeanAngle: 24)
            {
                Color = "Black"
            },
            new Bus("BUS456", seatingCapacity: 56)
            {
                Color = "Blue"
            },
            new Bus("BUS457", seatingCapacity: 56)
            {
                Color = "Green"
            },
            new Bus("BUS458", seatingCapacity: 40)
            {
                Color = "Yellow"
            },
            new Bus("BUS459", seatingCapacity: 32)
            {
                Color = "Yellow"
            },
            new Bus("BUS451", seatingCapacity: 22)
            {
                Color = "Red"
            },
            new Bus("BUS452", seatingCapacity: 22)
            {
                Color = "Red"
            },
            new Bus("BUS453", seatingCapacity: 18)
            {
                Color = "Red"
            },
            new Bus("BUS454", seatingCapacity: 92)
            {
                Color = "White"
            },
            ];
        int total = 0;
        foreach (Vehicle vehicle in vehicles)
        {
            try
            {
                _handler.AddVehicle(vehicle);
                total++;
            }
            catch (Exception e)
            {
                _ui.WriteError($"{e.Message} while adding {vehicle}");
            }
        }
        _ui.WriteLine($"Added {total} mock vehicles.");
    }
}
