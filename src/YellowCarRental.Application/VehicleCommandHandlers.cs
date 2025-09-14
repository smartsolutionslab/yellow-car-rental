using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public class VehicleCommandHandlers(IVehicles vehicles) : 
    IQueryCommandHandler<SearchVehiclesQueryCommand, SearchVehiclesQueryResult>
{
    public async Task<SearchVehiclesQueryResult> HandleQueryAsync(SearchVehiclesQueryCommand queryCommand)
    {
        var (period, stationId, category) = queryCommand;

        return new ([..(await vehicles.With(period, stationId, category)).ToData()]);
    }
}