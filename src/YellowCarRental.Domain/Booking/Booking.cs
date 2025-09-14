namespace SmartSolutionsLab.YellowCarRental.Domain;

public class Booking : IRootEntity
{
    public BookingIdentifier Id { get; private set; } = null!;
    public VehicleIdentifier VehicleId { get; private set; } = null!;
    public BookingCustomer Customer { get; private set; } = null!;
    public StationIdentifier PickupStationId { get; private set; } = null!;
    public StationIdentifier ReturnStationId { get; private set; } = null!;
    public DateRange Period { get; private set; } = null!;
    public Money TotalPrice { get; private set; } = null!;
    public BookingStatus Status { get; private set; }

    private Booking()
    {
    } // EF Core

    private Booking(
        VehicleIdentifier vehicleId, 
        BookingCustomer customer, 
        DateRange period, 
        StationIdentifier pickupStationId, 
        StationIdentifier returnStationId, 
        Money pricePerDay)
    {
        Id = BookingIdentifier.New();
        VehicleId = vehicleId;
        Customer = customer;
        Period = period;
        PickupStationId = pickupStationId;
        ReturnStationId = returnStationId;

        var days = period.TotalDaysInclusive();
        TotalPrice = Money.Of(days * pricePerDay.Amount, pricePerDay.Currency);
        Status = BookingStatus.Active;
    }

    public static Booking From(
        VehicleIdentifier vehicleId, 
        Customer customer, 
        DateRange period, 
        StationIdentifier pickupStationId, 
        StationIdentifier returnStationId, 
        Money pricePerDay)
    {
        return new Booking(
            vehicleId, 
            BookingCustomer.From(customer), 
            period, 
            pickupStationId, 
            returnStationId, 
            pricePerDay);
    }
    
    public static Booking From(
        VehicleIdentifier vehicleId, 
        BookingCustomer customer, 
        DateRange period, 
        StationIdentifier pickupStationId, 
        StationIdentifier returnStationId, 
        Money pricePerDay)
    {
        return new Booking(
            vehicleId, 
            customer, 
            period, 
            pickupStationId, 
            returnStationId, 
            pricePerDay);
    }
    
    public void Cancel()
    {
        if (Status == BookingStatus.Cancelled) throw new InvalidOperationException("Booking is already cancelled.");

        Status = BookingStatus.Cancelled;
    }
    
    public void Complete()
    {
        if (Status == BookingStatus.Completed) throw new InvalidOperationException("Booking is already completed.");
        if (Status == BookingStatus.Cancelled) throw new InvalidOperationException("Cancelled booking cannot be completed.");

        Status = BookingStatus.Completed;
    }
}