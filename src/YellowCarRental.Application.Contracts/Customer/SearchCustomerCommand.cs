namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

public sealed record SearchCustomerCommand(SearchTerm SearchTerm) : IQueryCommand;