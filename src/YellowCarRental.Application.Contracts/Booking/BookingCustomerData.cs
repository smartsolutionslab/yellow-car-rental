namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public class BookingCustomerData
{
    public BookingCustomerData() // Linq
    {}
    
    public required Guid Id { get; set; }
    public required string Salutation { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly BirthDate { get; set; }
}