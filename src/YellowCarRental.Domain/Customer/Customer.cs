namespace SmartSolutionsLab.YellowCarRental.Domain;

public class Customer: IRootEntity
{
    public CustomerIdentifier Id { get; private set; } = null!;
    
    public CustomerName Name { get; private set; } = null!;
    public BirthDate BirthDate { get; private set; } = null!;
    public CustomerAddress Address { get; private set; } = null!;
    public EMail EMail { get; private set; } = null!;

    private Customer() { } // EF Core

    private Customer(
        CustomerName name, 
        BirthDate birthDate, 
        CustomerAddress address, 
        EMail eMail)
    {
        Id = CustomerIdentifier.New();
        Name = name;
        BirthDate = birthDate;
        Address = address;
        EMail = eMail;
    }

    // for demo purposes only
    public void OverrideId(CustomerIdentifier id)
    {
        Id = id;
    }

    public static Customer From(
        CustomerName name, 
        BirthDate birthDate, 
        CustomerAddress address, 
        EMail eMail)
    {
        return new Customer(name, birthDate, address, eMail);
    }

    public static Customer From(
        string salutation,
        string firstName,
        string lastName,
        DateOnly birthDate,
        string street,
        string houseNumber,
        string zipCode,
        string city,
        string eMail)
    {
        return new Customer(
            CustomerName.From(salutation, firstName, lastName),
            BirthDate.From(birthDate),
            CustomerAddress.From(
                AddressStreet.From(street),
                HouseNumber.From(houseNumber),
                ZipCode.From(zipCode),
                City.From(city)),
            new EMail(eMail)
        );
    }
}
