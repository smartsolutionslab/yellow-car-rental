using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

public record ShowSimilarVehiclesCommand(
    VehicleIdentifier VehicleId,
    DateRange Period,
    StationIdentifier? StationId) : IQueryCommand;