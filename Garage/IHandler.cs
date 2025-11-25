using Garage.Vehicles;

namespace Garage;

public interface IHandler
{
    IEnumerable<Vehicle> GetAllVehicles();
    void AddVehicle(Vehicle vehicle);
    bool RemoveVehicle(Vehicle vehicle);
    Vehicle? GetVehicleByRegNr(string regnr);
    IEnumerable<Vehicle> SearchVehicle(string query);
}
