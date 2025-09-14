using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Frontend.Shared.Components;

public class RentalPriceCalculator
{
    public static Money CalcPrice(DateRange period, Money pricePerDay)
    {
        var money = Money.Of(pricePerDay.Amount * period.TotalDaysInclusive(), pricePerDay.Currency);
        return money;
    }
}