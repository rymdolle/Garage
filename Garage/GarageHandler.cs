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

    public bool RemoveVehicle(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Search for vehicles by color, type, or registration number.
    /// For example color=red,blue type=car,boat regnr=ABC123
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public IEnumerable<Vehicle> SearchVehicle(string query)
    {
        throw new NotImplementedException();
    }
}
