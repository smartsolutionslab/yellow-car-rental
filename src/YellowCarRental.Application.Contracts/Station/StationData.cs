namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;

public class StationData
{
    public required Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}