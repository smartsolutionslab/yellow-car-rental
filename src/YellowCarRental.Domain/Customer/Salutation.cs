namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record Salutation(string Value) : IValueObject
{
    public static readonly Salutation Mister = new("Herr");
    public static readonly Salutation Missus = new("Frau");
    public static readonly Salutation Divers = new("Divers");
    
    
    public static IEnumerable<Salutation> All
    {
        get
        {
            yield return Mister;
            yield return Missus;
            yield return Divers;
        }
    }
    
    public static Salutation From(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        
        if (value.Length > 20)
        {
            throw new ArgumentException("Salutation cannot be longer than 20 characters.", nameof(value));
        }

        return new Salutation(value);
    }
}