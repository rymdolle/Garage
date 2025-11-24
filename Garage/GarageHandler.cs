using Garage.Vehicles;

namespace Garage;

public class GarageHandler : IHandler
{
    public void AddVehicle(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public Vehicle? GetVehicleByRegNr(string regnr)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Vehicle> GetAllVehicles()
    {
        throw new NotImplementedException();
    }

    public int GetVehicleCountByType(string type)
    {
        throw new NotImplementedException();
    }

    public string[] GetVehicleTypes()
    {
        throw new NotImplementedException();
    }

    public bool RemoveVehicle(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Vehicle> SearchVehicle(string query)
    {
        throw new NotImplementedException();
    }
}
