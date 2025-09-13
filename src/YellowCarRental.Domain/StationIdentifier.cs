namespace SmartSolutionsLab.YellowCarRental.Domain;

public record StationIdentifier(Guid Id) : IValueObject
{
    public static StationIdentifier Of(Guid id) => new(id);
    public static StationIdentifier? Of(Guid? id) => id.HasValue ? new(id.Value) : null;

    public static StationIdentifier New() => new(Guid.CreateVersion7());
}