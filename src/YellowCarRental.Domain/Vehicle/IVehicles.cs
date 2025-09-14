using System.Xml.Linq;

namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface IVehicles : IRepository
{
    Task<IReadOnlyList<Vehicle>> WhichAreAvailable(
        DateRange period,
        StationIdentifier? stationId = null,
        VehicleCategory? category = null);
    
    Task<Vehicle> FindById(VehicleIdentifier vehicleId);
    
    Task<IReadOnlyCollection<Vehicle>> ByStationId(StationIdentifier stationId);
    Task<IReadOnlyList<Vehicle>> FindAll(IEnumerable<VehicleIdentifier> requestedVehicleIds);
    
    void Update(Vehicle vehicle);
    Task<IReadOnlyList<Vehicle>> WhichAreSimilar(
        DateRange period, 
        StationIdentifier? stationId, 
        VehicleCategory? category);
}