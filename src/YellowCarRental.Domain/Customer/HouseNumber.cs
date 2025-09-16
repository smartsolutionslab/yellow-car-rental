namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record HouseNumber(string Value) : IValueObject
{
    private HouseNumber() : this(string.Empty) // for EF
    { }
    public static HouseNumber From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("House number cannot be empty");
        return new HouseNumber(value.Trim());
    }
}