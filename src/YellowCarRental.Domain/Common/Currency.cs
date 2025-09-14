namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record Currency(string Code, string Symbol, int DecimalPlaces = 2) : IValueObject
{
    public static readonly Currency EUR = new("EUR", "€");
    public static readonly Currency USD = new("USD", "$");
    public static readonly Currency GBP = new("GBP", "£");
    
    public static IEnumerable<Currency> All
    {
        get
        {
            yield return EUR;
            yield return USD;
            yield return GBP;
        }
    }
    
    public static Currency FromKey(string code)
    {
        return All.Single(c => c.Code == code);
    }
}