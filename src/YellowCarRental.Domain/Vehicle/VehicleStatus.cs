namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record VehicleStatus(string Key, string Name) : IValueObject
{
    private VehicleStatus() : this(String.Empty, string.Empty) // For EF
    { }
    
    public static readonly VehicleStatus Available = new("Available", "Available");
    public static readonly VehicleStatus Rented = new("Rented", "Rented");
    
    public static IEnumerable<VehicleStatus> All
    {
        get
        {
            yield return Available;
            yield return Rented;
        }
    }
    
    public static VehicleStatus FromKey(string key)
    {
        return All.Single(c => c.Key == key);
    }
}