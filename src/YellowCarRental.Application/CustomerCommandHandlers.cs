using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public class CustomerCommandHandlers(ICustomers customers) :
    ICommandHandler<RegisterCustomerCommand, CustomerIdentifier>,
    IQueryCommandHandler<ShowAllCustomersCommand, ListCustomersQueryResult>,
    IQueryCommandHandler<ShowCustomerCommand, CustomerData>
{
    

    public async Task<CustomerIdentifier> HandleAsync(RegisterCustomerCommand command)
    {
        var (salutation, firstName, lastName, birthDate, address) = command;
        var customer = Customer.From(CustomerName.From(salutation, firstName, lastName), birthDate, address);
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
}