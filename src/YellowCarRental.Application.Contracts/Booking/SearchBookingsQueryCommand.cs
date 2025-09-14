using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public record SearchBookingsQueryCommand(
    DateRange Period, 
    StationIdentifier? StationId, 
    CustomerIdentifier? CustomerId) : IQueryCommand;

public record ListAllBookingsQueryCommand() : IQueryCommand;