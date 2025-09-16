namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record CustomerIdentifier(Guid Value) : IValueObject
{
    private CustomerIdentifier() : this(Guid.Empty) // for EF
    { }
    public static CustomerIdentifier Of(Guid id)
    {
        if(id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        return new CustomerIdentifier(id);
    }
    
    public static CustomerIdentifier Of(string id) => new(Guid.Parse(id)); 

    public static CustomerIdentifier? IfPossibleOf(Guid? id)
    {
        return id.HasValue && id != Guid.Empty ? Of(id.Value) : null;
    }
    
    public static CustomerIdentifier? IfPossibleOf(string? id)
    {
        if (id is null || string.IsNullOrWhiteSpace(id)) return null;

        return IfPossibleOf(Guid.Parse(id));
    }

    public static CustomerIdentifier New() => new(Guid.CreateVersion7());
}