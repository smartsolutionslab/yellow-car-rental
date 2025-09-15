using Microsoft.EntityFrameworkCore;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public static class StationExtensions
{
    public static Station Assign(this Station station, Vehicle vehicle)
    {   
        station.AssignVehicle(vehicle.Id);
        return station;
    }
}

public class Stations(RentalDbContext dbContext) : IStations
{
    public async Task<IList<Station>> All()
    {
        return await dbContext.Stations.AsNoTracking().ToListAsync();
    }

    public async Task<Station> FindById(StationIdentifier id)
    {
        var foundStation = await dbContext.Stations.FirstOrDefaultAsync(s => s.Id.Value == id.Value) ?? throw new KeyNotFoundException($"Station with ID {id} not found.");
        return foundStation;
    }

    public async Task Update(Station station)
    {
        dbContext.Update(station);
        await dbContext.SaveChangesAsync();
    }
}