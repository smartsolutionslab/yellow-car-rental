namespace SmartSolutionsLab.YellowCarRental.Domain;

public class Booking : IRootEntity
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateRange Period { get; private set; }
    public decimal TotalPrice { get; private set; }

    private Booking() { } // EF Core

    public Booking(Guid vehicleId, Guid customerId, DateRange period, decimal pricePerDay)
    {
        Id = Guid.NewGuid();
        VehicleId = vehicleId;
        CustomerId = customerId;
        Period = period;

        var days = period.TotalDaysInclusive();
        TotalPrice = days * pricePerDay;
    }
}