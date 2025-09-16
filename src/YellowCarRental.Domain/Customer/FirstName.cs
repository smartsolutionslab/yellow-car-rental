namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record FirstName(string Value) : IValueObject
{
    private FirstName() : this(string.Empty) // for EF
    { }
    
    public static FirstName From(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        
        if (value.Length > 50)
        {
            throw new ArgumentException("First name cannot be longer than 50 characters.", nameof(value));
        }

        return new FirstName(value);
    }
}