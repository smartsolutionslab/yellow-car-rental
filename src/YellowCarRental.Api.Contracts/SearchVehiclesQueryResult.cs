namespace SmartSolutionsLab.YellowCarRental.Api.Contracts;

public record SearchVehiclesQueryResult(List<VehicleData> Vehicles)
{
    public static SearchVehiclesQueryResult Empty => new (new List<VehicleData>());
}