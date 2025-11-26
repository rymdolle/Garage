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
        var car = new Car("ABC123", "Sedan");
        // Act
        garage.Add(car);
        // Assert
        Assert.Equal(1, garage.Count);
    }

    [Fact]
    public void Garage_AddVehicle_ExceedCapacity_Failure()
    {
        // Arrange
        var garage = new Garage<Vehicle>(1);
        var car1 = new Car("ABC123", "Sedan");
        var car2 = new Car("ABC124", "Sedan");
        // Act
        garage.Add(car1);
        // Assert
        Assert.Equal(1, garage.Count);
        Assert.Throws<InvalidOperationException>(() => garage.Add(car2));
    }

    [Fact]
    public void Garage_RemoveVehicle_Success()
    {
        // Arrange
        var garage = new Garage<Vehicle>(1);
        var car = new Car("ABC123", "Sedan");
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
        var car1 = new Car("ABC123", "Sedan");
        var car2 = new Car("ABC124", "Sedan");
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
        var car1 = new Car("ABC123", "Sedan");
        var car2 = new Car("ABC123", "Sedan"); // Same registration number
        // Act
        garage.Add(car1);
        // Assert
        Assert.Equal(1, garage.Count);
        Assert.Throws<InvalidOperationException>(() => garage.Add(car2));
    }

    [Fact]
    public void Garage_NumberPlateCaseSensitivity_Enforced()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        var car1 = new Car("abc123", "Sedan");
        var car2 = new Car("ABC123", "Sedan"); // Different case
        // Act
        garage.Add(car1);
        // Assert
        Assert.Equal(1, garage.Count);
        Assert.Throws<InvalidOperationException>(() => garage.Add(car2));
    }

    [Fact]
    public void Garage_FindByRegNr_ReturnsCorrectVehicle()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        string regnr = "XYZ789";
        var car1 = new Car("ABC123", "Sedan");
        var car2 = new Car(regnr, "Sedan");
        garage.Add(car1);
        garage.Add(car2);
        // Act
        var foundCar = garage.GetVehicleByRegNr(regnr);
        // Assert
        Assert.NotNull(foundCar);
        Assert.Equal(regnr, foundCar.RegistrationNumber);
    }

    [Fact]
    public void Garage_SearchByColor_SingleMatch()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        var car1 = new Car("ABC123", "Sedan")
        {
            Color = "Black"
        };
        var car2 = new Car("XYZ789", "Sedan")
        {
            Color = "Red"
        };
        garage.Add(car1);
        garage.Add(car2);
        // Act
        var redCars = garage.Where(v => (v as Car)!.Color == "Red").ToList();
        // Assert
        Assert.Single(redCars);
    }
}
