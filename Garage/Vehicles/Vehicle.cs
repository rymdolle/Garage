namespace Garage.Vehicles;

public abstract class Vehicle(string regnr)
{
    public string? Color { get; set; }
    public string RegistrationNumber { get; protected set; } = regnr;
    public override string ToString()
    {
        string color = (Color == null || Color == string.Empty) ? "N/A" : Color;
        return $"{GetType().Name} - RegNr: {RegistrationNumber}, Color: {color}";
    }
}
