using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public record SearchBookingsQueryCommand(
    DateRange Period, 
    SearchTerm? SearchTerm,
    StationIdentifier? StationId, 
    CustomerIdentifier? CustomerId) : IQueryCommand;