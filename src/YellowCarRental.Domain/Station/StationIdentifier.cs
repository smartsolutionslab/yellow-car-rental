namespace SmartSolutionsLab.YellowCarRental.Domain;

public record StationIdentifier(Guid Value) : IValueObject
{
    public static StationIdentifier Of(Guid id) => new(id);
    public static StationIdentifier? IfPossibleOf(Guid? id) => id.HasValue ? new(id.Value) : null;
    
    public static StationIdentifier Of(string id) => new(Guid.Parse(id));
    public static StationIdentifier? IfPossibleOf(string id) => !String.IsNullOrWhiteSpace(id) ? new(Guid.Parse(id)): null;

    public static StationIdentifier New() => new(Guid.CreateVersion7());
}