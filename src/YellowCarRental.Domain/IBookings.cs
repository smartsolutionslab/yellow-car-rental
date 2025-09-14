namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface IBookings : IRepository
{
    Task<IList<Booking>> All();
    
    Task<IList<Booking>> With(DateRange period, StationIdentifier? stationId, CustomerIdentifier? customerId);
    
    Task<Booking> FindById(BookingIdentifier bookingId);
    
    Task Add(Booking booking);
}