namespace Garage.Vehicles;

public abstract class Vehicle(string regnr)
{
    public string RegistrationNumber { get; protected set; } = regnr;
}
