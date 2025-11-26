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
                new Menu("Car", CreateCar),
                new Menu("Motorcycle", CreateMotorcycle),
                new Menu("Bus", CreateBus),
                new Menu("Airplane", CreateAirplane),
                new Menu("Boat", CreateBoat),
            ]),
            new Menu("Remove vehicle", RemoveVehicle),
            new Menu("Search vehicle", SearchVehicle),
            new Menu("Create mock vehicles", CreateMockVehicles),
            ]);
    }

    private void ListVehicleCountByType()
    {
        foreach (var (key, value) in _handler.VehicleCountByType())
        {
            _ui.WriteLine($"{key}: {value}");
        }
    }

    public void Run()
    {
        _ui.WriteLine("Welcome to Parkin Garage Application!");
        int capacity = _ui.ReadInt("Choose garage capacity:", c => c > 0, "Capacity has to be more than 0");
        _handler.CreateGarage(capacity);
        _mainMenu.Title += $" ({capacity})";

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

    private void CreateCar()
    {
        string regnr = _ui.ReadString("Enter registration number:", c => c != string.Empty, "Regnr can't be empty");
        string body = _ui.ReadString("Enter body type:", c => c != string.Empty, "Body type can't be empty");
        _handler.AddVehicle(new Car(regnr, body));
        _ui.WriteLine("Car created.");
    }
    private void CreateMotorcycle()
    {
        string regnr = _ui.ReadString("Enter registration number:", c => c != string.Empty, "Regnr can't be empty");
        int leanAngle = _ui.ReadInt("Enter max lean angle:", c => c > 0, "Lean angle has to be an integer greater than 0");
        _handler.AddVehicle(new Motorcycle(regnr, leanAngle));
        _ui.WriteLine("Motorcycle created.");

    }

    private void CreateBus()
    {
        string regnr = _ui.ReadString("Enter registration number:", c => c != string.Empty, "Regnr can't be empty");
        int seats = _ui.ReadInt("Enter seat capacity:", c => c > 0, "Capacity has to be an integer greater than 0");
        _handler.AddVehicle(new Motorcycle(regnr, seats));
        _ui.WriteLine("Bus created.");
    }

    private void CreateAirplane()
    {
        string regnr = _ui.ReadString("Enter registration number:", c => c != string.Empty, "Regnr can't be empty");
        int wingspan = _ui.ReadInt("Enter wing span:", c => c > 0, "Wing span has to be an integer greater than 0");
        _handler.AddVehicle(new Airplane(regnr, wingspan));
        _ui.WriteLine("Airplane created.");
    }

    private void CreateBoat()
    {
        string regnr = _ui.ReadString("Enter registration number:", c => c != string.Empty, "Regnr can't be empty");
        int displacement = _ui.ReadInt("Enter displacement:", c => c > 0, "Displacement has to be an integer greater than 0");
        _handler.AddVehicle(new Boat(regnr, displacement));
        _ui.WriteLine("Boat created.");
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
