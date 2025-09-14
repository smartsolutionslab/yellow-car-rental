using Microsoft.EntityFrameworkCore;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Customers : ICustomers
{
    private readonly RentalDbContext _dbContext;


    public  Customers(RentalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<IReadOnlyList<Customer>> All()
    {
        return await _dbContext.Customers.AsNoTracking().ToListAsync();
    }

    public async Task<Customer> FindById(CustomerIdentifier customerId)
    {
        var foundCustomer = _dbContext.Customers.AsNoTracking().FirstOrDefault(c => c.Id == customerId);

        await Task.CompletedTask;
        
        return foundCustomer ?? throw new PersistenceException($"Customer with id {customerId} not found");
    }

    public async Task Add(Customer customer)
    {
        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<Customer>> WithSearchTerm(SearchTerm searchTerm)
    {
        var matchingCustomers = await _dbContext.Customers.AsNoTracking()
            .Where(c => EF.Functions.Like(c.Name.FirstName.Value, searchTerm.Value) || 
                        EF.Functions.Like(c.Name.LastName.Value,searchTerm.Value) ||
                        EF.Functions.Like(c.Address.Street.Value, searchTerm.Value) ||
                        EF.Functions.Like(c.Address.City.Value, searchTerm.Value))
            .ToListAsync();

        return matchingCustomers;
    }
}