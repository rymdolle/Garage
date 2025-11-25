namespace Garage.Vehicles;

public class Bus(string regnr) : Vehicle(regnr)
{
    public int SeatingCapacity { get; set; }
    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber} - SeatingCapacity: {SeatingCapacity}";
    }
}
