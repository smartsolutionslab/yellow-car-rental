using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public sealed record BookVehicleCommand(
    VehicleIdentifier VehicleId,
    BookingCustomer Customer,
    StationIdentifier PickupStationId,
    StationIdentifier ReturnStationId,
    DateRange Period) : ICommand, IBookVehicleCommand;