using Garage.Vehicles;
using System.Collections;

namespace Garage;

public sealed class Garage<T> : IEnumerable<T> where T : Vehicle
{
    private readonly T?[] _parking;

    public int Capacity => _parking.Length;
    public int Count { get; private set; }
    public Garage(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1, "Capacity has to be at least 1");
        _parking = new T[capacity];
    }

    public IEnumerator<T> GetEnumerator()
    {
        int index = 0;
        foreach (var vehicle in _parking)
        {
            if (index >= Count)
                yield break;
            if (vehicle != null)
            {
                index++;
                yield return vehicle;
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Find vehicle by registration number.
    /// </summary>
    /// <param name="regnr"></param>
    /// <returns>Returns vehicle or null</returns>
    public T? GetVehicleByRegNr(string regnr)
    {
        return this.FirstOrDefault(v => regnr.Equals(v.RegistrationNumber, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Adds a vehicle to the garage if space is available and the registration number is unique.
    /// </summary>
    /// <param name="item">The vehicle to add to the garage.</param>
    /// <exception cref="InvalidOperationException">Thrown if the garage is full or if a vehicle with the same registration number already exists in the garage.</exception>
    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));
        if (Count >= Capacity)
            throw new InvalidOperationException("Garage is full");
        if (GetVehicleByRegNr(item.RegistrationNumber) != null)
            throw new InvalidOperationException("RegistrationNumber has to be unique");

        for (int i = 0; i < _parking.Length; i++)
        {
            // Insert into first spot that is null
            if (_parking[i] == null)
            {
                _parking[i] = item;
                Count++;
                return;
            }
        }

        throw new InvalidOperationException("Could not add item");
    }

    /// <summary>
    /// Remove vehicle from garage.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>True if operation is successful</returns>
    public bool Remove(T item)
    {
        for (int i = 0; i < _parking.Length; i++)
        {
            if (_parking[i] == item)
            {
                _parking[i] = null;
                Count--;
                return true;
            }
        }
        return false;
    }
}
