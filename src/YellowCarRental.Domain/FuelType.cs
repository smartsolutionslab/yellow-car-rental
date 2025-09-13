namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record FuelType(string Key, string Name)  : IValueObject
{
    public static readonly FuelType Petrol = new("P", "Petrol");
    public static readonly FuelType Diesel = new("D", "Diesel");
    public static readonly FuelType Electric = new("E", "Electric");
    public static readonly FuelType Hybrid = new("H", "Hybrid");
    public static readonly FuelType PluginHybrid = new("PH", "PluginHybrid");
}