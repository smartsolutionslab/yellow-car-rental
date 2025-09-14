using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public class StationsCommandHandlers(IStations stations) : IQueryCommandHandler<ListAllStationsQueryCommand, ListStationsQueryResult>
{
    public async Task<ListStationsQueryResult> HandleQueryAsync(ListAllStationsQueryCommand queryCommand)
    {
        return new ([..(await stations.All()).ToData()]);
    }
}