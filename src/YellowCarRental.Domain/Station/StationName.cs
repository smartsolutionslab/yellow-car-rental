namespace SmartSolutionsLab.YellowCarRental.Domain;

public record StationName(string Value) : IValueObject
{
    private StationName() : this(string.Empty) // for EF
    { }
    public static StationName From(string value) => new(value);
}