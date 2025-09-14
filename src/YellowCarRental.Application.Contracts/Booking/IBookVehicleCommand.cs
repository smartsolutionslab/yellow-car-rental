using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public interface IBookVehicleCommand
{
    VehicleIdentifier VehicleId { get; init; }
    BookingCustomer Customer { get; init; }
    StationIdentifier PickupStationId { get; init; }
    StationIdentifier ReturnStationId { get; init; }
    DateRange Period { get; init; }
}