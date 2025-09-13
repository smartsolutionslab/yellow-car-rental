namespace SmartSolutionsLab.YellowCarRental.Domain;

public record BookingIdentifier(Guid Value) : IValueObject
{
    public static BookingIdentifier Of(Guid value)
    {
        return new BookingIdentifier(value);
    }

    public static BookingIdentifier? Of(Guid? value)
    {
        return value.HasValue ? new BookingIdentifier(value.Value) : null;
    }

    public static BookingIdentifier New()
    {
        return new BookingIdentifier(Guid.CreateVersion7());
    }
}