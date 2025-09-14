using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface IBookings : IRepository
{
    Task<IReadOnlyList<Booking>> All();
    
    Task<IReadOnlyList<Booking>> With(
        DateRange? period, 
        SearchTerm? searchTerm,  
        StationIdentifier? stationId, 
        CustomerIdentifier? customerId);
    
    Task<IReadOnlyList<Booking>> ForVehicle(VehicleIdentifier vehicleId, DateRange period = null);
    
    Task<Booking> FindById(BookingIdentifier bookingId);
    
    Task Add(Booking booking);
    Task Update(Booking booking);
}