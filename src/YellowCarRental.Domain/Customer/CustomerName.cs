namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed record CustomerName(Salutation Salutation, FirstName FirstName, LastName LastName) : IValueObject
{
    private CustomerName() : this(new Salutation(string.Empty), new FirstName(string.Empty), new LastName(string.Empty)) // for EF
    { }
    
    public static CustomerName From(Salutation salutation, FirstName firstName, LastName lastName) =>
        new(salutation, firstName, lastName);
    
    public static CustomerName From(string salutation, string firstName, string lastName) =>
        new(Salutation.From(salutation),  FirstName.From(firstName), LastName.From(lastName));

    public static CustomerName Parse(string value)
    {
        var readItems = value.Split(',');
        var salutation = readItems[0];
        var first = readItems[1];
        var last = readItems[2];
        return CustomerName.From(new Salutation(salutation), new FirstName(first), new LastName(last));
    }
}