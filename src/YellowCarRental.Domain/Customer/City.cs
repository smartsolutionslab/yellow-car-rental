namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record City(string Value) : IValueObject
{
    public static City From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("City cannot be empty");
        return new City(value.Trim());
    }
}