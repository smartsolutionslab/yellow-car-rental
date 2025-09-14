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

public class Stations : IStations
{
    private readonly RentalDbContext _dbContext;

    public Stations(RentalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IList<Station>> All()
    {
        return await _dbContext.Stations.AsNoTracking().ToListAsync();
    }

    public async Task<Station> FindById(StationIdentifier id)
    {
        var foundStation = await _dbContext.Stations.FirstOrDefaultAsync(s => s.Id == id) ?? throw new KeyNotFoundException($"Station with ID {id} not found.");
        return foundStation;
    }

    public async Task Update(Station station)
    {
        _dbContext.Update(station);
        await _dbContext.SaveChangesAsync();
    }
}