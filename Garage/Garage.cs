using Garage.Vehicles;
using System.Collections;

namespace Garage;

public sealed class Garage<T> : IEnumerable<T> where T : Vehicle
{
    private readonly T?[] _parking;

    public int Capacity => _parking.Length;
    public int Count { get; private set; }
    public T? this[string regnr] => GetVehicleByRegNr(regnr);
    public Garage(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1, "Capacity has to be at least 1");
        _parking = new T[capacity];
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
            yield return _parking[i]!;
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Find vehicle by registration number.
    /// </summary>
    /// <param name="regnr"></param>
    /// <returns>Returns vehicle or null</returns>
    public T? GetVehicleByRegNr(string regnr)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(regnr);
        return this.FirstOrDefault(v => regnr.Equals(v.RegistrationNumber, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Adds a vehicle to the garage if space is available and the registration number is unique.
    /// </summary>
    /// <param name="item">The vehicle to add to the garage.</param>
    /// <exception cref="InvalidOperationException">Thrown if the garage is full or if a vehicle with the same registration number already exists in the garage.</exception>
    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentException.ThrowIfNullOrWhiteSpace(item.RegistrationNumber);
        if (Count >= Capacity)
            throw new InvalidOperationException("Garage is full");
        if (GetVehicleByRegNr(item.RegistrationNumber) != null)
            throw new InvalidOperationException("RegistrationNumber has to be unique");

        _parking[Count++] = item;
    }

    /// <summary>
    /// Remove vehicle from garage.
    /// </summary>
    /// <param name="regnr"></param>
    /// <returns>True if operation is successful</returns>
    public bool Remove(string regnr)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(regnr);
        for (int i = 0; i < Count; i++)
        {
            if (regnr.Equals(_parking[i]?.RegistrationNumber, StringComparison.OrdinalIgnoreCase))
            {
                Count--;
                _parking[i] = _parking[Count];
                _parking[Count] = null;
                return true;
            }
        }
        return false;
    }
}
