namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

public record SearchSimilarVehiclesQueryResult(List<VehicleData> Vehicles)
{
    public static SearchSimilarVehiclesQueryResult Empty => new ([]);
}