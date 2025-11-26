namespace Garage.Vehicles;

public abstract class Vehicle(string regnr)
{
    public string? Color { get; set; }
    public string RegistrationNumber { get; protected set; } = regnr;
    public override string ToString()
    {
        return $"{GetType().Name} - RegNr: {RegistrationNumber}, Color: {Color}";
    }
}
