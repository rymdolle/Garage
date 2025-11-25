namespace Garage.Vehicles;

public class Boat(string regnr, int displacement) : Vehicle(regnr)
{
    public int Displacement { get; set; } = displacement;

    public override string ToString()
    {
        return @$"{GetType().Name} - RegNr: {RegistrationNumber} - Displacement: {Displacement}";
    }
}
