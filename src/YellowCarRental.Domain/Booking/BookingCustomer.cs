namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record BookingCustomer(CustomerIdentifier Id, CustomerName Name, BirthDate BirthDate) : IValueObject
{
    public static BookingCustomer From(Customer customer)
    {
        return new BookingCustomer(customer.Id, customer.Name, customer.BirthDate);
    }
    
    public static BookingCustomer From(Guid id, string salutation, string firstName, string lastName, DateOnly birthDate)
    {
        return new BookingCustomer(
            CustomerIdentifier.Of(id), 
            CustomerName.From(salutation, firstName, lastName), 
            BirthDate.From(birthDate));
    }
}