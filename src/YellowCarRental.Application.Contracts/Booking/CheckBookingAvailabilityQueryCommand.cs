using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public record CheckBookingAvailabilityQueryCommand(
    VehicleIdentifier VehicleId, 
    DateRange Period) : IQueryCommand;