using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;

namespace SmartSolutionsLab.YellowCarRental.Api;

public static class StationsEndpoints
{
    public static void MapStationEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/stations", 
            (IQueryCommandHandler<ListAllStationsQueryCommand, ListStationsQueryResult> handler) =>
                handler.HandleQueryAsync(new ListAllStationsQueryCommand()));

    }
}