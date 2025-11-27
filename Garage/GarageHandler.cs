using Garage.Vehicles;

namespace Garage;

public class GarageHandler : IHandler
{
    private Garage<Vehicle> _garage = null!;

    public void CreateGarage(int capacity)
    {
        _garage = new Garage<Vehicle>(capacity);
    }

    public void AddVehicle(Vehicle vehicle)
    {
        _garage.Add(vehicle);
    }

    public Vehicle? GetVehicleByRegNr(string regnr)
    {
        return _garage.GetVehicleByRegNr(regnr);
    }

    public IEnumerable<Vehicle> GetAllVehicles()
    {
        return _garage;
    }

    public bool RemoveVehicle(Vehicle vehicle)
    {
        return _garage.Remove(vehicle);
    }

    /// <summary>
    /// Search for vehicles by color, type, or registration number.
    /// For example color=red,blue type=car,boat regnr=ABC123
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public IEnumerable<Vehicle> SearchVehicle(string query)
    {
        var filters = query.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(part => part.Split('=', 2))
            .Where(parts => parts.Length == 2)
            .ToDictionary(parts => parts[0].ToLower(),
                          parts => parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToLower()).ToList());
        IEnumerable<Vehicle> result = _garage;
        foreach (var filter in filters)
        {
            switch (filter.Key)
            {
                case "color":
                    result = result.Where(v => filter.Value.Contains(v.Color?.ToLower() ?? string.Empty));
                    break;
                case "type":
                    result = result.Where(v => filter.Value.Contains(v.GetType().Name.ToLower()));
                    break;
                case "regnr":
                    result = result.Where(v => filter.Value.Contains(v.RegistrationNumber.ToLower()));
                    break;
                case "make":
                    result = result.Where(v => filter.Value.Contains(v.Make?.ToLower() ?? string.Empty));
                    break;
                case "model":
                    result = result.Where(v => filter.Value.Contains(v.Model?.ToLower() ?? string.Empty));
                    break;
                default:
                    break;
            }
        }

        return result;
    }

    public Dictionary<string, int> VehicleCountByType()
    {
        Dictionary<string, int> counts = [];
        foreach (var vehicle in _garage)
        {
            string type = vehicle.GetType().Name;
            if (counts.TryGetValue(type, out int value))
                counts[type] = ++value;
            else
                counts[type] = 1;
        }
        return counts;
    }
}
