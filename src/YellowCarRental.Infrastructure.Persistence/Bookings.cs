using System.Reflection.Metadata.Ecma335;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Bookings : IBookings
{
    private readonly List<Booking> _bookings = new();
    
    public Task<IList<Booking>> All()
    {
        return Task.FromResult<IList<Booking>>(_bookings.ToList());
    }

    public Task<IList<Booking>> With(DateRange period, StationIdentifier? stationId, CustomerIdentifier? customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Booking>> ForVehicle(VehicleIdentifier vehicleId, DateRange period)
    {
        var query = _bookings.Where(booking => booking.VehicleId == vehicleId)
            .Where(booking => booking.Period.Start <= period.End && booking.Period.End >= period.Start);

        return Task.FromResult((IList<Booking>)query.ToList());
    }

    public Task<Booking> FindById(BookingIdentifier bookingId)
    {
        return Task.FromResult(_bookings.SingleOrDefault(booking => booking.Id == bookingId)!) ?? throw new Exception($"Booking with ID {bookingId} not found");
    }

    public Task Add(Booking booking)
    {
        _bookings.Add(booking);
        return Task.CompletedTask;
    }

    public Task Update(Booking booking)
    {
        // Do nothing for in-memory implementation
        return Task.CompletedTask;
    }
}