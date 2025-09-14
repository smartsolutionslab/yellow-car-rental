using Microsoft.AspNetCore.Mvc;
using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Api;

public static class VehiclesEndpoints
{
    public static void MapVehicleEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/vehicles/search",
            ([FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] Guid? stationId, [FromQuery] string? category,
                    [FromServices] IQueryCommandHandler<SearchAvailableVehiclesQueryCommand, SearchVehiclesQueryResult> handler) =>
                handler.HandleQueryAsync(new SearchAvailableVehiclesQueryCommand(
                    DateRange.From(start, end),
                    StationIdentifier.IfPossibleOf(stationId),
                    VehicleCategory.IfPossibleFromKey(category))));

        builder.MapGet("/api/vehicles/{vehicleId:guid}/similar",
            ([FromRoute] Guid vehicleId, [FromQuery] DateTime start, [FromQuery]DateTime end, [FromQuery] Guid? stationId,
                    [FromServices] IQueryCommandHandler<ShowSimilarVehiclesCommand, SearchSimilarVehiclesQueryResult> handler) =>
                handler.HandleQueryAsync(new ShowSimilarVehiclesCommand(
                    VehicleIdentifier.Of(vehicleId),
                    DateRange.From(start, end),
                    StationIdentifier.IfPossibleOf(stationId))));
        
        builder.MapGet("/api/vehicles/{vehicleId:guid}",
            ([FromRoute] Guid vehicleId, [FromServices] IQueryCommandHandler<ShowVehiclesCommand, VehicleData> handler) =>
                handler.HandleQueryAsync(new ShowVehiclesCommand(VehicleIdentifier.Of(vehicleId))));
    }
}