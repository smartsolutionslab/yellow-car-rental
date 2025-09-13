using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public record SearchVehiclesQueryCommand(DateRange Period, StationIdentifier? StationId, string? Category) : IQueryCommand;