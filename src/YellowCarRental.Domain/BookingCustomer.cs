namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record BookingCustomer(CustomerIdentifier Id, CustomerName Name, Email Email) : IValueObject;