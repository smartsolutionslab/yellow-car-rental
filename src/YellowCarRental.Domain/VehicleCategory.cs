namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record VehicleCategory(string Key, string Name) : IValueObject
{
    public static readonly VehicleCategory Compact = new("S", "Kleinwaen");
    public static readonly VehicleCategory Sedan = new("M", "Limousine");
    public static readonly VehicleCategory Suv = new("L", "SUV");
    public static readonly VehicleCategory Van = new("T", "Transporter");

    public static IEnumerable<VehicleCategory> All
    {
        get
        {
            yield return Compact;
            yield return Sedan;
            yield return Suv;
            yield return Van;
        }
    }
    
    public static VehicleCategory FromKey(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key, nameof(key));
        
        return All.Single(c => c.Key == key);
    }

    public static VehicleCategory? IfPossibleFromKey(string? key) =>
        !String.IsNullOrWhiteSpace(key) ? FromKey(key) : null;
}
