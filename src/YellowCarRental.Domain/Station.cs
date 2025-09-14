namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed class Station : IRootEntity
{
    public StationIdentifier Id { get; }
    public StationName Name { get; }


    private Station(StationIdentifier id, StationName name)
    {
        Id = id;
        Name = name;
    }

    public static Station From(StationIdentifier id, StationName name) => new(id, name);
}