using Microsoft.EntityFrameworkCore;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Bookings(RentalDbContext dbContext) : IBookings
{
    public async Task<IReadOnlyList<Booking>> All()
    {
        return await dbContext.Bookings.AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<Booking>> With(
        DateRange? period, 
        SearchTerm? searchTerm, 
        StationIdentifier? stationId, 
        CustomerIdentifier? customerId)
    {
        var query = dbContext.Bookings.AsQueryable();

        if (period is not null)
        {
            query = query.Where(booking => booking.Period.Start <= period.End && booking.Period.End >= period.Start);
        }

        if (stationId is not null)
        {
            query = query.Where(booking => booking.PickupStationId == stationId || booking.ReturnStationId == stationId);
        }
        
        if (customerId is not null)
        {
            query = query.Where(booking => booking.Customer.Id == customerId);
        }
        
        if (searchTerm is not null)
        {
            query = query.Where(booking => EF.Functions.Like(booking.Customer.Name.FirstName.Value, searchTerm.Value) ||
                                           EF.Functions.Like(booking.Customer.Name.LastName.Value, searchTerm.Value));

        }



        return await query.AsNoTracking().ToListAsync();

    }

    public async Task<IReadOnlyList<Booking>> ForVehicle(VehicleIdentifier vehicleId, DateRange period)
    {
        await Task.CompletedTask;
        
        var query = dbContext.Bookings.Where(booking => booking.VehicleId == vehicleId)
            .Where(booking => booking.Period.Start <= period.End && booking.Period.End >= period.Start);

        var foundBookings = await query.AsNoTracking().ToListAsync();

        return foundBookings;
    }

    public async Task<Booking> FindById(BookingIdentifier bookingId)
    {
        var foundBooking = await dbContext.Bookings.SingleOrDefaultAsync(booking => booking.Id == bookingId);
        
        return foundBooking ?? throw new PersistenceException($"Booking with ID {bookingId} not found");
    }

    public async Task Add(Booking booking)
    {
        dbContext.Bookings.Add(booking);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(Booking booking)
    {
        dbContext.Update(booking);
        await dbContext.SaveChangesAsync();
    }
}