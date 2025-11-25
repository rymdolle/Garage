using System.Drawing;

namespace Garage.Vehicles;

public class Car(string regnr, string? color = null) : Vehicle(regnr)
{
    public string? Color { get; set; } = color;
    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber}, Color: {Color ?? "Undefined"}";
    }
}
