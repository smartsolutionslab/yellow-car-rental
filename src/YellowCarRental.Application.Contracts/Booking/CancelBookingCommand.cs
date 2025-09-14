using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public sealed record CancelBookingCommand(BookingIdentifier BookingId) : ICommand;