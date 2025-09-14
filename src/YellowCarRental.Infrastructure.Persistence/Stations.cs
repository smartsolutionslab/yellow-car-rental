using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Stations : IStations
{
    public Task<IList<Station>> All()
    {
        List<Station> stations =
        [
            Station.From(StationIdentifier.Of("f9a3c9e-2b91-4c61-9f58-8dfe5a9a3b7c"), StationName.From("Berlin")),
            Station.From(StationIdentifier.Of("a2d7b8e1-5f3c-4a9b-b2cd-1e8f6d2c7a34"), StationName.From("München")),
            Station.From(StationIdentifier.Of("6c1f2a44-0e2d-4760-8b7a-3c9d1a5f0e21"), StationName.From("Hamburg")),
            Station.From(StationIdentifier.Of("d9e4c2b7-8a1f-4d3e-a456-2f1b3c7d9e80"), StationName.From("Frankfurt")),
            Station.From(StationIdentifier.Of("0fb2a9cd-73e0-41be-9c12-5f84a6d3b1f9"), StationName.From("Stuttgart"))
        ];

        return Task.FromResult<IList<Station>>(stations);
    }
}