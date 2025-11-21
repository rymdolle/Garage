namespace Garage.Vehicles;

public class Car(string regnr) : Vehicle(regnr)
{
    public string? Color { get; set; }
}
