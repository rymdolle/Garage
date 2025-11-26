namespace Garage.Vehicles;

public abstract class Vehicle(string regnr, string? color = null)
{
    public string? Color { get; set; } = color;
    public string RegistrationNumber { get; protected set; } = regnr;
    public override string ToString()
    {
        return $"{GetType().Name} - RegNr: {RegistrationNumber}, Color: {Color}";
    }
}
