namespace SmartSolutionsLab.YellowCarRental.Domain;

public record BookingIdentifier(Guid Value) : IValueObject
{
    public static BookingIdentifier Of(Guid id)
    {
        if(id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        return new BookingIdentifier(id);
    }
    
    public static BookingIdentifier Of(string id) => new(Guid.Parse(id)); 

    public static BookingIdentifier? IfPossibleOf(Guid? id)
    {
        return id.HasValue && id != Guid.Empty ? Of(id.Value) : null;
    }
    
    public static BookingIdentifier? IfPossibleOf(string? id)
    {
        if (id is null || string.IsNullOrWhiteSpace(id)) return null;

        return IfPossibleOf(Guid.Parse(id));
    }

    public static BookingIdentifier New() => new(Guid.CreateVersion7());
}