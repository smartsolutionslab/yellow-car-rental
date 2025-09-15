namespace SmartSolutionsLab.YellowCarRental.Domain;

public record Money(decimal Amount, string Currency) : IValueObject
{
    private Money(): this(0m, "EUR") // for EF
    { }
    
    public static Money Of(decimal amount, string currency = "EUR")
    {
        return new Money(amount, currency);
    }
}