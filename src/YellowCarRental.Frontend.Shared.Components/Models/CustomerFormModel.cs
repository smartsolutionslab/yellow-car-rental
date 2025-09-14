using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Frontend.Shared.Components.Models;

public class CustomerFormModel
{
    public Guid Id { get; set; }
    public Salutation Salutation { get; set; } = Salutation.Mister;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

}