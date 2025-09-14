namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

public record SearchVehiclesQueryResult(List<VehicleData> Vehicles)
{
    public static SearchVehiclesQueryResult Empty => new ([]);
}