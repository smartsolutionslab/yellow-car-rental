namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public record SearchBookingsQueryResult(List<BookingData> Bookings)
{
    public static SearchBookingsQueryResult Empty => new([]);
}