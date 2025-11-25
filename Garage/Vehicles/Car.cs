namespace Garage.Vehicles;

public class Car(string regnr, string bodyType) : Vehicle(regnr)
{
    public string BodyType { get; set; } = bodyType;
    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber}, BodyType: {BodyType}";
    }
}
