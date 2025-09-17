using Microsoft.EntityFrameworkCore;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Customers(RentalDbContext dbContext) : ICustomers
{
    public async Task<IReadOnlyList<Customer>> All()
    {
        return await dbContext.Customers.AsNoTracking().ToListAsync();
    }

    public async Task<Customer> FindById(CustomerIdentifier customerId)
    {
        var foundCustomer = dbContext.Customers
            .AsNoTracking()
            .FirstOrDefault(c => c.Id.Value == customerId.Value);

        await Task.CompletedTask;
        
        return foundCustomer ?? throw new PersistenceException($"Customer with id {customerId} not found");
    }

    public async Task Add(Customer customer)
    {
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Customer>> WithSearchTerm(SearchTerm searchTerm)
    {
        var matchingCustomers = await dbContext.Customers.AsNoTracking()
            .Where(c => c.Name.FirstName.Value.Contains(searchTerm.Value) || 
                        c.Name.LastName.Value.Contains(searchTerm.Value) ||
                        c.Address.Street.Value.Contains(searchTerm.Value) ||
                        c.Address.City.Value.Contains(searchTerm.Value))
            .ToListAsync();

        return matchingCustomers;
    }
}