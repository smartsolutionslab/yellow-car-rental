namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface IVehicles : IRepository
{
    Task<IEnumerable<Vehicle>> With(
        DateRange period,
        StationIdentifier? stationId = null,
        string? category = null);
}