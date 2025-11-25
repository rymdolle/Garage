namespace Garage.Vehicles;

public class Motorcycle(string regnr, int maxLeanAngle) : Vehicle(regnr)
{
    public int MaxLeanAngle { get; set; } = maxLeanAngle;

    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber} - MaxLeanAngle: {MaxLeanAngle}";
    }
}
