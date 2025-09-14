namespace SmartSolutionsLab.YellowCarRental.Domain;

public record VehicleName(string Value) : IValueObject
{
    public static VehicleName Of(string name)
    {
        return new VehicleName(name);
    }
}