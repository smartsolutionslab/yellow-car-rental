namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

public class VehicleData
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Seats { get; set; }
    public string Fuel { get; set; } = string.Empty;
    public string Transmission { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
}