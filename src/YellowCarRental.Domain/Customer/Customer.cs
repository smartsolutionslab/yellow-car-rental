namespace SmartSolutionsLab.YellowCarRental.Domain;

public class Customer: IRootEntity
{
    public CustomerIdentifier Id { get; private set; }
    
    public CustomerName Name { get; private set; } = null!;
    public BirthDate BirthDate { get; private set; }
    public CustomerAddress Address { get; private set; } = null!;

    private Customer() { } // EF Core

    private Customer(CustomerName name, BirthDate birthDate, CustomerAddress address)
    {
        Id = CustomerIdentifier.New();
        Name = name;
        BirthDate = birthDate;
        Address = address;
    }

    public static Customer From(CustomerName name, BirthDate birthDate, CustomerAddress address)
    {
        return new Customer(name, birthDate, address);
    }
    
    public static Customer From(string salutation, string firstName, string lastName, DateOnly birthDate, string street, string houseNumber, string zipCode, string city)
    {
        return new Customer(
            CustomerName.From(salutation, firstName, lastName),
            BirthDate.From(birthDate),
            CustomerAddress.From(
                AddressStreet.From(street),
                HouseNumber.From(houseNumber),
                ZipCode.From(zipCode),
                City.From(city)
            )
        );
    }
}