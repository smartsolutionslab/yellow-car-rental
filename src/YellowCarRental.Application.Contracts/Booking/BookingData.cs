namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public class BookingData
{
    public required Guid Id { get; set; }
    public required Guid VehicleId { get; set; }
    public required BookingCustomerData Customer { get; set; }
    public required Guid PickupStationId { get; set; }
    public required Guid ReturnStationId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required decimal TotalPrice { get; set; }
    public required string TotalPriceCurrency { get; set; }
}