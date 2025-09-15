namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record AddressStreet(string Value) : IValueObject
{
    public static AddressStreet From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Street cannot be empty");
        return new AddressStreet(value.Trim());
    }
}