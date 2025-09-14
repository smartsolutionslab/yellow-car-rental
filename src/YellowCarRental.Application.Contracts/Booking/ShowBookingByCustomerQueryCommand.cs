using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public record ShowBookingByCustomerQueryCommand(
    CustomerIdentifier CustomerId) : IQueryCommand;