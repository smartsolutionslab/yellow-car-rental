using System.Text.Json;

namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record BookingCustomer(
    CustomerIdentifier Id, 
    CustomerName Name, 
    BirthDate BirthDate) : IValueObject
{
    private BookingCustomer() // for EF
        : this(
            new CustomerIdentifier(Guid.Empty),
            new CustomerName(
                new Salutation(string.Empty),
                new FirstName(string.Empty),
                new LastName(string.Empty)),
            new BirthDate(DateOnly.MinValue))
    {}
    
    public static BookingCustomer From(Customer customer)
    {
        return new BookingCustomer(customer.Id, customer.Name, customer.BirthDate);
    }
    
    public static BookingCustomer From(
        Guid id, 
        string salutation, 
        string firstName, 
        string lastName, 
        DateOnly birthDate)
    {
        return new BookingCustomer(
            CustomerIdentifier.Of(id), 
            CustomerName.From(salutation, firstName, lastName), 
            BirthDate.From(birthDate));
    }
}