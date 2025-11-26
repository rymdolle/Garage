namespace Garage.Vehicles;

public class Bus(string regnr, int seatingCapacity) : Vehicle(regnr)
{
    public int SeatingCapacity { get; set; } = seatingCapacity;
    public override string ToString()
    {
        return $"{base.ToString()}, SeatingCapacity: {SeatingCapacity}";
    }
}
