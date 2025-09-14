using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

public sealed record ShowCustomerCommand(CustomerIdentifier CustomerId) : IQueryCommand;