namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record ZipCode(string Value) : IValueObject
{
    private ZipCode(): this(string.Empty) // for EF
    { }
    
    public static ZipCode From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Zip code cannot be empty");
        return new ZipCode(value.Trim());
    }
}