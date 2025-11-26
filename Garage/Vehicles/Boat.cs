namespace Garage.Vehicles;

public class Boat(string regnr, int displacement) : Vehicle(regnr)
{
    public int Displacement { get; set; } = displacement;

    public override string ToString()
    {
        return $"{base.ToString()}, Displacement: {Displacement}";
    }
}
