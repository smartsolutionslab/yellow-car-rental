namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record CustomerIdentifier(Guid Value) : IValueObject
{
    public static CustomerIdentifier Of(Guid id) => new(id);
    public static CustomerIdentifier? IfPossibleOf(Guid? id) => id.HasValue ? new(id.Value) : null;

    public static CustomerIdentifier Of(string id) => new(Guid.Parse(id));
    public static CustomerIdentifier? IfPossibleOf(string id) => !String.IsNullOrWhiteSpace(id) ? new(Guid.Parse(id)): null;

    public static CustomerIdentifier New() => new(Guid.CreateVersion7());
}