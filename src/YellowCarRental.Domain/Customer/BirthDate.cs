namespace SmartSolutionsLab.YellowCarRental.Domain;
public sealed record BirthDate(DateOnly Value) : IValueObject
{
    private BirthDate() : this(DateOnly.MinValue) // for EF
    {}
    
    public static BirthDate From(DateOnly value)
    {
        if (value > DateOnly.FromDateTime(DateTime.UtcNow.Date))
            throw new ArgumentException("Birthdate cannot be in the future");
        return new BirthDate(value);
    }
}