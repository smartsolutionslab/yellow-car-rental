using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public static class DateRangeExpressions
{
    /// <summary>
    /// SQL-taugliche Expression für Overlap-Prüfung
    /// </summary>
    public static Expression<Func<Booking, bool>> Overlaps(DateOnly start, DateOnly end)
    {
        return booking => booking.Period.Start < end && start < booking.Period.End;
    }
}



public class Vehicles(RentalDbContext dbContext) : IVehicles
{
    public async Task<IReadOnlyList<Vehicle>> WhichAreAvailable(
        DateRange period,
        StationIdentifier? stationId = null,
        VehicleCategory? category = null)
    {
        var query = dbContext.Vehicles.AsQueryable();

        if (category is not null)
        {
            query = query.Where(v => v.Category.Key == category.Key);
        }

        if (stationId is not null)
        {
            query = query.Where(vehicle => vehicle.StationId.Value == stationId.Value);
        }
/*
        var bookingsByVehicleIdQuery = (VehicleIdentifier vehicleId) => dbContext.Bookings
            .Where(b => b.VehicleId.Value == vehicleId.Value)
            .Where(b => b.Status != BookingStatus.Cancelled);
*/
        var possibleVehicleIds = query.Select(vehicle => vehicle.Id.Value).Distinct();

        var possibleRelatedOverlappingBookingsByPeriodQuery = dbContext.Bookings
            .AsNoTracking()
            .Where(b => b.Status != BookingStatus.Cancelled)
            .Where(DateRangeExpressions.Overlaps(period.Start, period.End))
            .Where(b => possibleVehicleIds.Contains(b.VehicleId.Value));

        query = query.Where(vehicle =>
            possibleRelatedOverlappingBookingsByPeriodQuery.All(b => b.VehicleId.Value != vehicle.Id.Value));
            

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<Vehicle> FindById(VehicleIdentifier vehicleId)
    {
        var foundVehicle = await dbContext.Vehicles.SingleOrDefaultAsync(v => v.Id.Value == vehicleId.Value);
        

        return foundVehicle ?? throw new PersistenceException($"Vehicle with ID {vehicleId} not found");
    }

    public async Task<IReadOnlyCollection<Vehicle>> ByStationId(StationIdentifier stationId)
    {
        var foundVehicles = await dbContext.Vehicles
            .Where(v => v.StationId.Value == stationId.Value)
            .ToListAsync();
        
        return foundVehicles;
    }

    public async Task<IReadOnlyList<Vehicle>> FindAll(IEnumerable<VehicleIdentifier> requestedVehicleIds)
    {
        var vehicleIds = requestedVehicleIds.Select(id => id.Value).ToList();
        
        return await dbContext.Vehicles.Where(vehicle => vehicleIds.Contains(vehicle.Id.Value)).ToListAsync();
    }

    public void Update(Vehicle vehicle)
    {
        // In-memory, so nothing to do here
    }

    public async Task<IReadOnlyList<Vehicle>> WhichAreSimilar(DateRange period, StationIdentifier? stationId,
        VehicleCategory? category)
    {
        // Hint: good case to handle cancelation tokens
        
        var normalSearch = await WhichAreAvailable(period, stationId, category);

        if (normalSearch.Any())
        {
            var normalIds = normalSearch.Select(v => v.Id.Value);
            
            if (stationId is not null)
            {
                var firstTryResult = await WhichAreAvailable(period, null, category);
                var similarResult = firstTryResult.Where(vehicle => !normalIds.Contains(vehicle.Id.Value)).ToList();

                if (similarResult.Any())
                {
                    return new List<Vehicle>(similarResult);
                }
            }

            if (category is not null)
            {
                var secondTryResult = await WhichAreAvailable(period, stationId, null);
                var similarResult = secondTryResult.Where(vehicle => !normalIds.Contains(vehicle.Id.Value)).ToList();
                if (similarResult.Any())
                {
                    return new List<Vehicle>(similarResult);
                }
            }

            // other period
            var lastTryResult = await WhichAreAvailable(
                DateRange.From(
                    period.Start.AddDays(period.TotalDaysInclusive()* -1), 
                    period.End.AddDays(period.TotalDaysInclusive())),
                stationId, category);
            
            var lastResult = lastTryResult.Where(vehicle => !normalIds.Contains(vehicle.Id.Value)).ToList();

            return new List<Vehicle>(lastResult);
        }
        else
        {
            if (category is not null)
            {
                var firstTryResult = await WhichAreAvailable(period, stationId, null);
                
                if (firstTryResult.Any())
                {
                    return new List<Vehicle>(firstTryResult);
                }
            }

            if (stationId is not null)
            {
                var secondTryResult = await WhichAreAvailable(period, null, category);

                if (secondTryResult.Any())
                {
                    return new List<Vehicle>(secondTryResult);
                }
            }
            
            // other period
            var lastTryResult = await WhichAreAvailable(
                DateRange.From(
                    period.Start.AddDays(period.TotalDaysInclusive()* -1), 
                    period.End.AddDays(period.TotalDaysInclusive())),
                stationId, category);

            return new List<Vehicle>(lastTryResult);
        }
    }
}