namespace SmartSolutionsLab.YellowCarRental.Domain;

public record VehicleName(string Value) : IValueObject
{
    private VehicleName() : this(String.Empty) // For EF
    {}

    public static VehicleName Of(string name)
    {
        return new VehicleName(name);
    }
}