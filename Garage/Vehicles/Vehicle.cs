using static Garage.Vehicles.Vehicle;

namespace Garage.Vehicles;

public abstract class Vehicle(string regnr)
{
    public string RegistrationNumber { get; protected set; } = regnr;
    public abstract override string ToString();
}
