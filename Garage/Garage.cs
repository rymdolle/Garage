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
        foreach (var vehicle in _parking)
        {
            if (vehicle != null)
            {
                yield return vehicle;
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Add vehicle to garage.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>True if operation is successful</returns>
    public bool Add(T item)
    {
        if (Count >= Capacity)
            return false;
        if (_parking.Any(v => item.RegistrationNumber.Equals(v?.RegistrationNumber, StringComparison.OrdinalIgnoreCase)))
            return false;

        for (int i = 0; i < _parking.Length; i++)
        {
            // Insert into first spot that is null
            if (_parking[i] == null)
            {
                _parking[i] = item;
                Count++;
                return true;
            }
        }

        return false;
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
