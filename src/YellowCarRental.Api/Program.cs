using SmartSolutionsLab.YellowCarRental.Api.Contracts;
using SmartSolutionsLab.YellowCarRental.Application;
using SmartSolutionsLab.YellowCarRental.Domain;
using SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterAllApplicationCommandsAndHandlers();
builder.AddPersistence();
builder.AddServiceDefaults();

var app = builder.Build();

app.MapGet("/api/vehicles/search", 
    (DateTime start, DateTime end, Guid? stationId, string? category, IQueryCommandHandler<SearchVehiclesQueryCommand, SearchVehiclesQueryResult> handler) =>
        handler.HandleQueryAsync(new SearchVehiclesQueryCommand(
            DateRange.From(start, end),
            StationIdentifier.Of(stationId), 
            category)));

app.Run();

