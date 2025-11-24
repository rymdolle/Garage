namespace Garage.Vehicles;

public class Car(string regnr) : Vehicle(regnr, VehicleType.Car)
{
    public string? Color { get; set; }
}
