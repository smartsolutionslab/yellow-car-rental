namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record TransmissionType(string Key, string Name) : IValueObject
{
    public static readonly TransmissionType Manual = new("M", "Manual");
    public static readonly TransmissionType Automatic = new("A", "Automatic");
    
    public static IEnumerable<TransmissionType> All
    {
        get
        {
            yield return Manual;
            yield return Automatic;
        }
    }
    
    public static TransmissionType FromKey(string key)
    {
        return All.Single(c => c.Key == key);
    }
}