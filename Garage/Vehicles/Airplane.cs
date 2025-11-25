namespace Garage.Vehicles;

public class Airplane(string regnr, int wingspan) : Vehicle(regnr)
{
    public int WingSpan { get; set; } = wingspan;

    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber} - WingSpan: {WingSpan}";
    }
}
