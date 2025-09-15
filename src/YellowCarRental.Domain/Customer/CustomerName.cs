namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record CustomerName(Salutation Salutation, FirstName FirstName, LastName LastName) : IValueObject
{
    private CustomerName() : this(null!, null!, null!) // for EF
    { }
    
    public static CustomerName From(Salutation salutation, FirstName firstName, LastName lastName) =>
        new(salutation, firstName, lastName);
    
    public static CustomerName From(string salutation, string firstName, string lastName) =>
        new(Salutation.From(salutation),  FirstName.From(firstName), LastName.From(lastName));
}