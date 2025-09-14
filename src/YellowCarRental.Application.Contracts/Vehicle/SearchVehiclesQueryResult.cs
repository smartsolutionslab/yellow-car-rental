namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

public record SearchVehiclesQueryResult(
    List<VehicleData> FoundVehicles, 
    List<VehicleData> SimilarVehicles)
{
    public static SearchVehiclesQueryResult Empty => new ([], []);
}