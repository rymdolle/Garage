namespace Garage.Vehicles;

public class Airplane(string regnr) : Vehicle(regnr)
{
    public int WingSpan { get; set; }

    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber} - WingSpan: {WingSpan}";
    }
}
