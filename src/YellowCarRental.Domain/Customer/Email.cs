namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record EMail(string Value) : IValueObject
{
    private EMail() : this(string.Empty) // for EF
    { }
    
}