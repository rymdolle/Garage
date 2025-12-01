using Garage.Extensions;
using Garage.UserInterface;
using Garage.Vehicles;
using Microsoft.Extensions.Configuration;

namespace Garage;

public class Manager
{
    private IUserInterface _ui;
    private IHandler _handler;
    private IConfiguration _config;

    private Menu _mainMenu;
    public Manager(IConfiguration config, IUserInterface ui, IHandler handler)
    {
        _ui = ui;
        _handler = handler;
        _config = config;

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
        if (_config.GetValue<bool>("demo", false) && _ui.ReadString("Do you want to populate with demo vehicles? [y/N]").Equals("y", StringComparison.CurrentCultureIgnoreCase))
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
        string regnr = _ui.ReadString("Registration number (empty to abort):", c => c == string.Empty || _handler.GetVehicleByRegNr(c) == null, "Registration number has to be unique");
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

        vehicle.Make = _ui.ReadString("Make [Enter to skip]:").ValueOrDefault();
        vehicle.Model = _ui.ReadString("Model [Enter to skip]:").ValueOrDefault();
        vehicle.Color = _ui.ReadString("Color [Enter to skip]:").ValueOrDefault();

        _ui.WriteLine(vehicle.ToString());
        string confirm = _ui.ReadString("Is this correct? [Y/n]");
        if (confirm.ToLower().Equals("n"))
        {
            _ui.WriteError("Aborted.");
            return;
        }
        _handler.AddVehicle(vehicle);
        _ui.WriteLine($"{vehicle.GetType().Name} parked.");
    }


    private void RemoveVehicle()
    {
        _ui.WriteLine("Enter registration number of the vehicle to remove:");
        string regnr = _ui.ReadLine();
        if (_handler.RemoveVehicle(regnr))
        {
            _ui.WriteLine($"Vehicle {regnr} removed from the garage.");
        }
        else
        {
            _ui.WriteError($"Failed to remove vehicle {regnr}.");
        }
    }

    private void SearchVehicle()
    {
        _ui.WriteLine("Enter search query (e.g. color=red,blue,white type=car,airplane regnr=ABC123):");
        _ui.WriteLine("Available filters: color, type, regnr, make, model");
        string query = _ui.ReadLine();
        var results = _handler.SearchVehicle(query).ToList();

        _ui.WriteLine($"Search Results: {results.Count}");
        foreach (var vehicle in results)
        {
            _ui.WriteLine(vehicle.ToString());
        }
    }

    private void ListAllVehicles()
    {
        var vehicles = _handler.GetAllVehicles().ToList();
        if (vehicles.Count == 0)
        {
            _ui.WriteLine("No vehicles parked in the garage.");
            return;
        }
        foreach (var vehicle in vehicles)
        {
            _ui.WriteLine(vehicle.ToString());
        }
        _ui.WriteLine($"Total vehicles: {vehicles.Count}");
    }

    private void CreateMockVehicles()
    {
        Vehicle[] vehicles = [
            new Car("ABC123", "Sedan")
            {
                Color = "Red",
                Make = "Toyota",
                Model = "Camry",
            },
            new Car("DEF456", "SUV")
            {
                Color = "Blue",
                Make = "Honda",
                Model = "CR-V",
            },
            new Motorcycle("XYZ789", maxLeanAngle: 24)
            {
                Color = "Black",
                Make = "Yamaha",
                Model = "YZF-R3",
            },
            new Motorcycle("MOTO123", maxLeanAngle: 30)
            {
                Color = "Red",
                Make = "Ducati",
                Model = "Panigale V2",
            },
            new Bus("BUS456", seatingCapacity: 56)
            {
                Color = "Blue",
                Make = "Mercedes-Benz",
                Model = "Citaro",
            },
            new Bus("BUS123", seatingCapacity: 56)
            {
                Color = "Green",
                Make = "Volvo",
                Model = "7900 Electric",
            },
            new Bus("BUS789", seatingCapacity: 40)
            {
                Color = "Yellow",
                Make = "Scania",
                Model = "Citywide",
            },
            new Airplane("AIR123", wingspan: 60)
            {
                Color = "White",
                Make = "Boeing",
                Model = "737",
            },
            new Airplane("AIR456", wingspan: 45)
            {
                Color = "Gray",
                Make = "Airbus",
                Model = "A320",
            },
            new Airplane("AIR789", wingspan: 35)
            {
                Color = "Silver",
                Make = "Cessna",
                Model = "172",
            },
            new Boat("BOAT123", displacement: 3000)
            {
                Color = "White",
                Make = "Bayliner",
                Model = "Element E18",
            },
            new Boat("BOAT456", displacement: 5000)
            {
                Color = "Blue",
                Make = "Yamaha",
                Model = "212X",
            },
            new Boat("BOAT789", displacement: 7000)
            {
                Color = "Red",
                Make = "Sea Ray",
                Model = "SPX 190",
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
