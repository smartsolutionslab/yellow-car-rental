namespace SmartSolutionsLab.YellowCarRental.Api.Contracts;

public class VehicleData
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Seats { get; set; }
    public string Fuel { get; set; } = string.Empty;
    public string Transmission { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public decimal TotalPrice { get; set; }
}