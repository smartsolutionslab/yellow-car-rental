namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface IBookings : IRepository
{
    Task<IList<Booking>> All();
    
    Task<IList<Booking>> With(DateRange period, StationIdentifier? stationId, CustomerIdentifier? customerId);
    
    Task<IList<Booking>> ForVehicle(VehicleIdentifier vehicleId, DateRange period = null);
    
    Task<Booking> FindById(BookingIdentifier bookingId);
    
    Task Add(Booking booking);
    Task Update(Booking booking);
}