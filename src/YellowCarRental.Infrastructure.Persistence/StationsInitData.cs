using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public static class StationsInitData
{
    public static IEnumerable<Station> AllStations => [Berlin, Muenchen, Hamburg, Frankfurt, Stuttgart];
    
    public static readonly Station Berlin = Station.From(
        StationIdentifier.Of("d8f4e7c2-3b1a-4e56-9d8f-2a7c1b3e9d4a"),
        StationName.From("Berlin"),
        StationAddress.From(
            AddressStreet.From("Alexanderplatz 1"),
            ZipCode.From("10178"),
            City.From("Berlin")));

    public static readonly Station Muenchen = Station.From(
        StationIdentifier.Of("a6b7c8d9-e1f2-4a3b-8c7d-9e0f1a2b3c4d"),
        StationName.From("München"),
        StationAddress.From(
            AddressStreet.From("Leopoldstraße 50"),
            ZipCode.From("80802"),
            City.From("München")));

    public static readonly Station Hamburg = Station.From(
        StationIdentifier.Of("5f6e7d8c-9b0a-4c3d-8e7f-6a5b4c3d2e1f"),
        StationName.From("Hamburg"),
        StationAddress.From(
            AddressStreet.From("Jungfernstieg 24"),
            ZipCode.From("20354"),
            City.From("Hamburg")));

    public static readonly Station Frankfurt = Station.From(
        StationIdentifier.Of("d9e4c2b7-8a1f-4d3e-a456-2f1b3c7d9e80"),
        StationName.From("Frankfurt"),
        StationAddress.From(
            AddressStreet.From("Theodor-Heuss-Allee 15"),
            ZipCode.From("60486"),
            City.From("Frankfurt am Main")));

    public static readonly Station Stuttgart = Station.From(
        StationIdentifier.Of("0fb2a9cd-73e0-41be-9c12-5f84a6d3b1f9"),
        StationName.From("Stuttgart "),
        StationAddress.From(
            AddressStreet.From("Königstraße 20"),
            ZipCode.From("70173"),
            City.From("Stuttgart")));
}