namespace SmartSolutionsLab.YellowCarRental.Domain;

public record Money(decimal Amount, string Currency) : IValueObject
{
    public static Money Of(decimal amount, string currency = "EUR")
    {
        return new Money(amount, currency);
    }
}