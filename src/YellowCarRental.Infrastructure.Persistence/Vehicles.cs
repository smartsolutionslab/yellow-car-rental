using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

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
            query = query.Where(v => v.Category == category);
        }

        if (stationId is not null)
        {
            query = query.Where(vehicle => vehicle.StationId == stationId);
        }

        query = query.Where(vehicle => dbContext.Bookings
            .Where(b => b.VehicleId.Value == b.VehicleId.Value)
            .Where(b => b.Period.Overlaps(period)).Any() == false);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<Vehicle> FindById(VehicleIdentifier vehicleId)
    {
        var foundVehicle = await dbContext.Vehicles.SingleOrDefaultAsync(v => v.Id == vehicleId);
        

        return foundVehicle ?? throw new PersistenceException($"Vehicle with ID {vehicleId} not found");
    }

    public async Task<IReadOnlyCollection<Vehicle>> ByStationId(StationIdentifier stationId)
    {
        var foundVehicles = await dbContext.Vehicles
            .Where(v => v.StationId == stationId)
            .ToListAsync();
        
        return foundVehicles;
    }

    public async Task<IReadOnlyList<Vehicle>> FindAll(IEnumerable<VehicleIdentifier> requestedVehicleIds)
    {
        return await dbContext.Vehicles.Where(vehicle => requestedVehicleIds.Contains(vehicle.Id)).ToListAsync();
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
            var normalIds = normalSearch.Select(v => v.Id);
            
            if (stationId is not null)
            {
                var firstTryResult = await WhichAreAvailable(period, null, category);
                var similarResult = firstTryResult.Where(vehicle => !normalIds.Contains(vehicle.Id)).ToList();

                if (similarResult.Any())
                {
                    return new List<Vehicle>(similarResult);
                }
            }

            if (category is not null)
            {
                var secondTryResult = await WhichAreAvailable(period, stationId, null);
                var similarResult = secondTryResult.Where(vehicle => !normalIds.Contains(vehicle.Id)).ToList();
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
            
            var lastResult = lastTryResult.Where(vehicle => !normalIds.Contains(vehicle.Id)).ToList();

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