namespace SmartSolutionsLab.YellowCarRental.Domain;

public record StationIdentifier(Guid Value) : IValueObject
{
    private StationIdentifier() : this(Guid.Empty) // for EF
    {}
    
    public static StationIdentifier Of(Guid id)
    {
        if(id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        return new StationIdentifier(id);
    }
    
    public static StationIdentifier Of(string id) => new(Guid.Parse(id)); 

    public static StationIdentifier? IfPossibleOf(Guid? id)
    {
        return id.HasValue && id != Guid.Empty ? Of(id.Value) : null;
    }
    
    public static StationIdentifier? IfPossibleOf(string? id)
    {
        if (id is null || string.IsNullOrWhiteSpace(id)) return null;

        return IfPossibleOf(Guid.Parse(id));
    }

    public static StationIdentifier New() => new(Guid.CreateVersion7());
}