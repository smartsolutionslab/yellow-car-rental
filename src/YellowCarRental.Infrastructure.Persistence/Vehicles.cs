using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Vehicles : IVehicles
{
    public Task<IEnumerable<Vehicle>> With(DateRange period, StationIdentifier? stationId = null, string? category = null)
    {
        throw new NotImplementedException();
    }
}


public static class Extensions
{
    public static TBuilder AddPersistence<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddScoped<IVehicles, Vehicles>();

        return builder;
    }
}