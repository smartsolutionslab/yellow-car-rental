namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

public class CustomerData
{
    public Guid Id { get; set; }
    public string Salutation { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}