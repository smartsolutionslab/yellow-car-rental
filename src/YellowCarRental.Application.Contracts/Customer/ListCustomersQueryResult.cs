namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

public record ListCustomersQueryResult(IList<CustomerData> Customers)
{
    public static ListCustomersQueryResult Empty => new ([]);
}