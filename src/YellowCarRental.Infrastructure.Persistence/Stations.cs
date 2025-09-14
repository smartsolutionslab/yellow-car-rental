using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public static class StationExtensions
{
    public static Station Assign(this Station station, Vehicle vehicle)
    {   
        station.AssignVehicle(vehicle.Id);
        return station;
    }
}


public class Stations : IStations
{
    public static readonly Station Berlin = Station.From(
        StationIdentifier.Of("d8f4e7c2-3b1a-4e56-9d8f-2a7c1b3e9d4a"),
        StationName.From("Berlin"))
        .Assign(Vehicles.Bmw320DBlau)
        .Assign(Vehicles.Bmw118IWeiss)
        .Assign(Vehicles.VwGolfWeiss)
        .Assign(Vehicles.AudiQ5Schwarz)
        .Assign(Vehicles.MercedesGlaSilber);

    public static readonly Station Muenchen = Station.From(
        StationIdentifier.Of("a6b7c8d9-e1f2-4a3b-8c7d-9e0f1a2b3c4d"),
        StationName.From("München"))
        .Assign(Vehicles.Bmw320DRot)
        .Assign(Vehicles.Bmw118IBlau)
        .Assign(Vehicles.VwGolfRot)
        .Assign(Vehicles.AudiQ5Gruen)
        .Assign(Vehicles.MercedesGlaRot);
    
    public static readonly Station Hamburg = Station.From(
        StationIdentifier.Of("5f6e7d8c-9b0a-4c3d-8e7f-6a5b4c3d2e1f"), 
        StationName.From("Hamburg"))
        .Assign(Vehicles.Bmw320DSchwarz)
        .Assign(Vehicles.Bmw118ISilber)
        .Assign(Vehicles.VwGolfSchwarz)
        .Assign(Vehicles.AudiQ5Grau)
        .Assign(Vehicles.MercedesGlaBlau);

    public static readonly Station Frankfurt = Station.From(
        StationIdentifier.Of("d9e4c2b7-8a1f-4d3e-a456-2f1b3c7d9e80"), 
        StationName.From("Frankfurt"))
        .Assign(Vehicles.Bmw320DWeiss)
        .Assign(Vehicles.Bmw118IRot)
        .Assign(Vehicles.VwGolfSilber)
        .Assign(Vehicles.AudiQ5Weiss)
        .Assign(Vehicles.MercedesGlaWeiss);


    public static readonly Station Stuttgart = Station.From(
        StationIdentifier.Of("0fb2a9cd-73e0-41be-9c12-5f84a6d3b1f9"),
        StationName.From("Stuttgart"))
        .Assign(Vehicles.Bmw320DGruen)
        .Assign(Vehicles.Bmw118ISchwarz)
        .Assign(Vehicles.VwGolfGelb)
        .Assign(Vehicles.AudiQ5Blau)
        .Assign(Vehicles.MercedesGlaHellblau);


    private readonly List<Station> _stations = [Berlin, Muenchen, Hamburg, Frankfurt, Stuttgart];
    
    
    
    
    public Task<IList<Station>> All()
    {
        return Task.FromResult<IList<Station>>(_stations.ToList());
    }

    public async Task<Station> FindById(StationIdentifier id)
    {
        var foundStation = _stations.FirstOrDefault(s => s.Id == id) ?? throw new KeyNotFoundException($"Station with ID {id} not found.");

        return await Task.FromResult(foundStation);
    }

    public Task Update(Station station)
    {
        // Do nothing, as this is an in-memory implementation

        return Task.CompletedTask;
    }
}