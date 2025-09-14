namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface ICustomers : IRepository
{
    Task<IList<Customer>> All();
    Task<Customer> FindById(CustomerIdentifier id);
    Task Add(Customer customer);
}