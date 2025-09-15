namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record CustomerAddress(AddressStreet Street, HouseNumber HouseNumber, ZipCode ZipCode, City City) : IValueObject
{
    public static CustomerAddress From(string street, string houseNumber, string zipCode, string city) =>
        new CustomerAddress(AddressStreet.From(street), HouseNumber.From(houseNumber), ZipCode.From(zipCode), City.From(city));
    public static CustomerAddress From(AddressStreet street, HouseNumber houseNumber, ZipCode zipCode, City city) =>
        new CustomerAddress(street, houseNumber, zipCode, city);
}