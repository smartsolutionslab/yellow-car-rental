namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record FuelType(string Key, string Name)  : IValueObject
{
    private FuelType() : this(String.Empty,string.Empty) // For EF
    {}
    
    public static readonly FuelType Petrol = new("P", "Petrol");
    public static readonly FuelType Diesel = new("D", "Diesel");
    public static readonly FuelType Electric = new("E", "Electric");
    public static readonly FuelType Hybrid = new("H", "Hybrid");
    public static readonly FuelType PluginHybrid = new("PH", "PluginHybrid");
    
    public static IEnumerable<FuelType> All
    {
        get
        {
            yield return Petrol;
            yield return Diesel;
            yield return Electric;
            yield return Hybrid;
            yield return PluginHybrid;
        }
    }
    
    public static FuelType FromKey(string key)
    {
        return All.Single(c => c.Key == key);
    }
}