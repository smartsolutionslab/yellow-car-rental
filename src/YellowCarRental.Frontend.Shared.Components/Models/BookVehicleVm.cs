using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Frontend.Shared.Components.Models;

public class BookVehicleVm : IBookVehicleCommand
{
    public VehicleIdentifier VehicleId { get; init; }
    public BookingCustomer Customer { get; init; }
    public StationIdentifier PickupStationId { get; init; }
    public StationIdentifier ReturnStationId { get; init; }
    public DateRange Period { get; init; }
}