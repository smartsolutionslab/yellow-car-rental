using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

public record SearchCustomersQueryResult(List<CustomerData> Customers)
{
    public static SearchCustomersQueryResult Empty => new ([]);
}