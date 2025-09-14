namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface IVehicles : IRepository
{
    Task<IList<Vehicle>> With(
        DateRange period,
        StationIdentifier? stationId = null,
        VehicleCategory? category = null);
    
    Task<Vehicle> FindById(VehicleIdentifier vehicleId);
}