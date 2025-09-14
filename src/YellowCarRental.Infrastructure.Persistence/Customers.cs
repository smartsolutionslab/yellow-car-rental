using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Customers : ICustomers
{
    public Task<IList<Customer>> All()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> FindById(CustomerIdentifier customerId)
    {
        throw new NotImplementedException();
    }

    public Task Add(Customer customer)
    {
        throw new NotImplementedException();
    }
}