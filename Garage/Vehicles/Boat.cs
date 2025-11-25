namespace Garage.Vehicles;

public class Boat(string regnr) : Vehicle(regnr)
{
    public int Displacement { get; set; }

    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber} - Displacement: {Displacement}";
    }
}
