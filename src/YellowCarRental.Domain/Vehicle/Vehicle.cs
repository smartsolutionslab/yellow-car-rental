namespace SmartSolutionsLab.YellowCarRental.Domain;

public class Vehicle : IRootEntity
{
    private Vehicle()
    {
    } // EF Core

    private Vehicle(VehicleName name, VehicleCategory category, FuelType fuel, TransmissionType transmission, Money pricePerDay)
    {
        Id = VehicleIdentifier.New(); // Todo: chekf if it so working with EF Core later
        Name = name;
        Category = category;
        Fuel = fuel;
        Transmission = transmission;
        PricePerDay = pricePerDay;
    }
  
    public static Vehicle From(VehicleName name, VehicleCategory category, FuelType fuel, TransmissionType transmission, Money pricePerDay)
    {
        return new Vehicle(name, category, fuel, transmission, pricePerDay);
    }

    public static Vehicle From(string name, VehicleCategory category, FuelType fuel, TransmissionType transmission, decimal pricePerDay)
    {
        return new Vehicle(VehicleName.Of(name), category, fuel, transmission, Money.Of(pricePerDay, Currency.EUR.Code));
    }

    public VehicleIdentifier Id { get; private set; } = null!;
    public VehicleName Name { get; private set; } = null!;
    public VehicleCategory Category { get; private set; } = null!;
    
    public FuelType Fuel { get; private set; } = null!;
    public TransmissionType Transmission { get; private set; } = null!;
    public Money PricePerDay { get; private set; } = null!;
}