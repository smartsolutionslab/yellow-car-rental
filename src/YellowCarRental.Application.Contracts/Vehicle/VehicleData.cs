namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

public class VehicleData
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Category { get; set; } = string.Empty;
    public required string Fuel { get; set; } = string.Empty;
    public required string Transmission { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
}