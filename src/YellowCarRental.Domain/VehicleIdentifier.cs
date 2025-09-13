namespace SmartSolutionsLab.YellowCarRental.Domain;

public record VehicleIdentifier(Guid Value) : IValueObject
{
    public static VehicleIdentifier Of(Guid value)
    {
        return new VehicleIdentifier(value);
    }

    public static VehicleIdentifier? Of(Guid? value)
    {
        return value.HasValue ? new VehicleIdentifier(value.Value) : null;
    }

    public static VehicleIdentifier New()
    {
        return new VehicleIdentifier(Guid.CreateVersion7());
    }
}