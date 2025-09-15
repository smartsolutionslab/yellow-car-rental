namespace SmartSolutionsLab.YellowCarRental.Domain;

public record VehicleIdentifier(Guid Value) : IValueObject
{
    public static VehicleIdentifier Of(Guid id)
    {
        if(id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        return new VehicleIdentifier(id);
    }
    
    public static VehicleIdentifier Of(string id) => new(Guid.Parse(id)); 

    public static VehicleIdentifier? IfPossibleOf(Guid? id)
    {
        return id.HasValue && id != Guid.Empty ? Of(id.Value) : null;
    }
    
    public static VehicleIdentifier? IfPossibleOf(string? id)
    {
        if (id is null || string.IsNullOrWhiteSpace(id)) return null;

        return IfPossibleOf(Guid.Parse(id));
    }

    public static VehicleIdentifier New() => new(Guid.CreateVersion7());
}