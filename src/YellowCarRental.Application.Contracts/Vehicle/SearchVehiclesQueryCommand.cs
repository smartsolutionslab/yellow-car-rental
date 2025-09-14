using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

public record SearchVehiclesQueryCommand(
    DateRange Period, 
    StationIdentifier? StationId, 
    VehicleCategory? Category) : IQueryCommand;