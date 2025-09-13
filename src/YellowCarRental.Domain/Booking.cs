namespace SmartSolutionsLab.YellowCarRental.Domain;

public class Booking : IRootEntity
{
    public BookingIdentifier Id { get; private set; }
    public VehicleIdentifier VehicleId { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateRange Period { get; private set; }
    public Money TotalPrice { get; private set; }

    private Booking()
    {
    } // EF Core

    private Booking(VehicleIdentifier vehicleId, Guid customerId, DateRange period, Money pricePerDay)
    {
        Id = BookingIdentifier.New();
        VehicleId = vehicleId;
        CustomerId = customerId;
        Period = period;

        var days = period.TotalDaysInclusive();
        TotalPrice = Money.Of(days * pricePerDay.Amount, pricePerDay.Currency);
    }

    public static Booking From(VehicleIdentifier vehicleId, Guid customerId, DateRange period, Money pricePerDay)
    {
        return new Booking(vehicleId, customerId, period, pricePerDay);
    }
}