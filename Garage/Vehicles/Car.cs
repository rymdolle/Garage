namespace Garage.Vehicles;

public class Car(string regnr, Car.BodyType body) : Vehicle(regnr)
{
    public BodyType Body { get; set; } = body;
    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber}, BodyType: {Body}";
    }

    public enum BodyType
    {
        Undefined,
        Sedan,
        Coupe,
        Hatchback,
        SUV,
        Convertible,
        Wagon,
        Van,
        Truck,
    }
}
