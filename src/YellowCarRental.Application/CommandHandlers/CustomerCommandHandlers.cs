using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public class CustomerCommandHandlers(ICustomers customers) :
    ICommandHandler<RegisterCustomerCommand, CustomerIdentifier>,
    IQueryCommandHandler<ShowAllCustomersCommand, ListCustomersQueryResult>,
    IQueryCommandHandler<ShowCustomerCommand, CustomerData>,
    IQueryCommandHandler<SearchCustomerCommand, SearchCustomersQueryResult>
{
    public async Task<CustomerIdentifier> HandleAsync(RegisterCustomerCommand command)
    {
        var (name, birthDate, address, eMail) = command;
        
        var customer = Customer.From(name, birthDate, address, eMail);
        await customers.Add(customer);
        
        return customer.Id;
    }

    public async Task<ListCustomersQueryResult> HandleQueryAsync(ShowAllCustomersCommand command)
    {
        return new([..(await customers.All()).ToData()]);
    }

    public async Task<CustomerData> HandleQueryAsync(ShowCustomerCommand command)
    {
        var customer = await customers.FindById(command.CustomerId);

        return customer.ToData();
    }


    public async Task<SearchCustomersQueryResult> HandleQueryAsync(SearchCustomerCommand command)
    {
        var matchingCustomers = await customers.WithSearchTerm(command.SearchTerm);

        return new SearchCustomersQueryResult([..matchingCustomers.ToData()]);
    }
}