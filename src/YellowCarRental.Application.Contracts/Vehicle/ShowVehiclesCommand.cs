using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

public record ShowVehiclesCommand(
    VehicleIdentifier VehicleId) : IQueryCommand;