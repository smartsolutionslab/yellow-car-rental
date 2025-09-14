using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

public record SearchTerm(string Value): IValueObject
{
    public static SearchTerm? IfPossibleOf(string? searchTerm)
    {
        if(string.IsNullOrEmpty(searchTerm)) return null;

        return new SearchTerm(searchTerm);
    }
}