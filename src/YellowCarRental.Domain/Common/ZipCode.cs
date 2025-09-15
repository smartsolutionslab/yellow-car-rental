namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record ZipCode(string Value) : IValueObject
{
    public static ZipCode From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Zip code cannot be empty");
        return new ZipCode(value.Trim());
    }
}