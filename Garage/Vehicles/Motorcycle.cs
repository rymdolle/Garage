namespace Garage.Vehicles;

public class Motorcycle(string regnr) : Vehicle(regnr)
{
    public int MaxLeanAngle { get; set; }

    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber} - MaxLeanAngle: {MaxLeanAngle}";
    }
}
