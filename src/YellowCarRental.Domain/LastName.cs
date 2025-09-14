namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record LastName(string Value) : IValueObject
{
    public static LastName From(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        
        if (value.Length > 50)
        {
            throw new ArgumentException("Last name cannot be longer than 50 characters.", nameof(value));
        }

        return new LastName(value);
    }
}