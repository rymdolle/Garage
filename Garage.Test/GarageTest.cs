using Garage.Vehicles;

namespace Garage.Test;

public class GarageTest
{
    [Fact]
    public void Garage_NegativeCapacity_ExceptionThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Garage<Vehicle>(-1));
    }

    [Fact]
    public void Garage_AddVehicle_Success()
    {
        // Arrange
        var garage = new Garage<Vehicle>(1);
        var car = new Car("ABC123");
        // Act
        var result = garage.Add(car);
        // Assert
        Assert.True(result);
        Assert.Equal(1, garage.Count);
    }

    [Fact]
    public void Garage_AddVehicle_ExceedCapacity_Failure()
    {
        // Arrange
        var garage = new Garage<Vehicle>(1);
        var car1 = new Car("ABC123");
        var car2 = new Car("ABC124");
        // Act
        garage.Add(car1);
        var result = garage.Add(car2);
        // Assert
        Assert.False(result);
        Assert.Equal(1, garage.Count);
    }

    [Fact]
    public void Garage_RemoveVehicle_Success()
    {
        // Arrange
        var garage = new Garage<Vehicle>(1);
        var car = new Car("ABC123");
        garage.Add(car);
        // Act
        var result = garage.Remove(car);
        // Assert
        Assert.True(result);
        Assert.Equal(0, garage.Count);
    }

    [Fact]
    public void Garage_RemoveVehicle_NotFound_Failure()
    {
        // Arrange
        var garage = new Garage<Vehicle>(1);
        var car1 = new Car("ABC123");
        var car2 = new Car("ABC124");
        garage.Add(car1);
        // Act
        var result = garage.Remove(car2);
        // Assert
        Assert.False(result);
        Assert.Equal(1, garage.Count);
    }

    [Fact]
    public void Garage_UniqueRegistrationNumber_Enforced()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        var car1 = new Car("ABC123");
        var car2 = new Car("ABC123"); // Same registration number
        // Act
        var result1 = garage.Add(car1);
        var result2 = garage.Add(car2);
        // Assert
        Assert.True(result1);
        Assert.False(result2);
        Assert.Equal(1, garage.Count);
    }

    [Fact]
    public void Garage_NumberPlateCaseSensitivity_Enforced()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        var car1 = new Car("abc123");
        var car2 = new Car("ABC123"); // Different case
        // Act
        var result1 = garage.Add(car1);
        var result2 = garage.Add(car2);
        // Assert
        Assert.True(result1);
        Assert.False(result2);
        Assert.Equal(1, garage.Count);
    }
}
