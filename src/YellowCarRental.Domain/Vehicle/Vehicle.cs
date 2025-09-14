namespace SmartSolutionsLab.YellowCarRental.Domain;

public class Vehicle : IRootEntity
{
    private Vehicle()
    {
    } // EF Core

    private Vehicle(
        VehicleName name, 
        VehicleCategory category, 
        FuelType fuel, 
        TransmissionType transmission, 
        Money pricePerDay, 
        StationIdentifier stationId)
    {
        Id = VehicleIdentifier.New(); // Todo: chekf if it so working with EF Core later
        Name = name;
        Category = category;
        Fuel = fuel;
        Transmission = transmission;
        PricePerDay = pricePerDay;
        StationId = stationId;
    }
  
    public static Vehicle From(
        VehicleName name, 
        VehicleCategory category, 
        FuelType fuel, 
        TransmissionType transmission, 
        Money pricePerDay,
        StationIdentifier stationId)
    {
        return new Vehicle(name, category, fuel, transmission, pricePerDay, stationId);
    }

    public static Vehicle From(
        string name, 
        VehicleCategory category, 
        FuelType fuel, 
        TransmissionType transmission, 
        decimal pricePerDay, 
        StationIdentifier stationId)
    {
        return new Vehicle(
            VehicleName.Of(name), 
            category, 
            fuel, 
            transmission,
            Money.Of(pricePerDay, Currency.EUR.Code), 
            stationId);
    }

    public VehicleIdentifier Id { get; private set; } = null!;
    public VehicleName Name { get; private set; } = null!;
    public VehicleCategory Category { get; private set; } = null!;
    
    public FuelType Fuel { get; private set; } = null!;
    public TransmissionType Transmission { get; private set; } = null!;
    public Money PricePerDay { get; private set; } = null!;
    
    public StationIdentifier StationId { get; private set; } = null!;
}