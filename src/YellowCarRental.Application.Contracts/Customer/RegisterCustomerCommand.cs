using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

public sealed record RegisterCustomerCommand(
    CustomerName Name,
    BirthDate BirthDate,
    CustomerAddress Address,
    EMail EMail) : ICommand;