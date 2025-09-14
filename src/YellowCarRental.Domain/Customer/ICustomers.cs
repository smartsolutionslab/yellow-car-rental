using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface ICustomers : IRepository
{
    Task<IReadOnlyList<Customer>> All();
    Task<Customer> FindById(CustomerIdentifier id);
    Task Add(Customer customer);
    Task<IReadOnlyCollection<Customer>> WithSearchTerm(SearchTerm searchTerm);
}