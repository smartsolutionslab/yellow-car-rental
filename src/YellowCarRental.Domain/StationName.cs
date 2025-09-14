namespace SmartSolutionsLab.YellowCarRental.Domain;

public record StationName(string Value) : IValueObject
{
    public static StationName From(string value) => new(value);
}