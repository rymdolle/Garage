using static Garage.Vehicles.Vehicle;

namespace Garage.Vehicles;

public abstract class Vehicle(string regnr, VehicleType type = VehicleType.Unknown)
{
    public string RegistrationNumber { get; protected set; } = regnr;
    public VehicleType Type { get; } = type;

    public enum VehicleType
    {
        Unknown,
        Car,
    }
}
