namespace SmartSolutionsLab.YellowCarRental.Domain;

public class Vehicle : IRootEntity
{
    private Vehicle()
    {
    } // EF Core

    private Vehicle(VehicleName name, VehicleCategory category, int seats, FuelType fuel, TransmissionType transmission, Money pricePerDay)
    {
        Id = VehicleIdentifier.New(); // Todo: chekf if it so working with EF Core later
        Name = name;
        Category = category;
        Seats = seats;
        Fuel = fuel;
        Transmission = transmission;
        PricePerDay = pricePerDay;
    }

    public static Vehicle From(VehicleName name, VehicleCategory category, int seats, FuelType fuel, TransmissionType transmission, Money pricePerDay)
    {
        return new Vehicle(name, category, seats, fuel, transmission, pricePerDay);
    }

    public VehicleIdentifier Id { get; private set; }
    public VehicleName Name { get; private set; }
    public VehicleCategory Category { get; private set; }
    public int Seats { get; private set; }
    public FuelType Fuel { get; private set; }
    public TransmissionType Transmission { get; private set; }
    public Money PricePerDay { get; private set; }
}