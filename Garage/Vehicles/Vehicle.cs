using System.Text;

namespace Garage.Vehicles;

public abstract class Vehicle(string regnr)
{
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public string RegistrationNumber { get; protected set; } = regnr;
    public override string ToString()
    {
        StringBuilder sb = new($"{GetType().Name} - RegNr: {RegistrationNumber}");
        if (Make != null && Make != string.Empty)
        {
            sb.Append($", Make: {Make}");
        }

        if (Model != null && Model != string.Empty)
        {
            sb.Append($", Model: {Model}");
        }

        if (Color != null && Color != string.Empty)
        {
            sb.Append($", Color: {Color}");
        }

        return sb.ToString();
    }
}
