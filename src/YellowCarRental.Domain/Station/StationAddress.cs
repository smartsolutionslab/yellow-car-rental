namespace SmartSolutionsLab.YellowCarRental.Domain;

public record StationAddress(AddressStreet Street, ZipCode ZipCode, City City)
{
    public static StationAddress From(AddressStreet street, ZipCode zipCode, City city)
    {
        return new StationAddress(street, zipCode, city);
    }
}