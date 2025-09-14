using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public class VehicleCommandHandlers(IVehicles vehicles) : 
    IQueryCommandHandler<SearchAvailableVehiclesQueryCommand, SearchVehiclesQueryResult>,
    IQueryCommandHandler<ShowSimilarVehiclesCommand, SearchVehiclesQueryResult>,
    IQueryCommandHandler<ShowVehiclesCommand, VehicleData>
{
    public async Task<SearchVehiclesQueryResult> HandleQueryAsync(SearchAvailableVehiclesQueryCommand queryCommand)
    {
        var (period, stationId, category) = queryCommand;

        return new(
            [..(await vehicles.WhichAreAvailable(period, stationId, category)).ToData()],
            [..(await vehicles.WhichAreSimilar(period, stationId, category)).ToData()]
        );
    }
    
    public async Task<SearchVehiclesQueryResult> HandleQueryAsync(ShowSimilarVehiclesCommand queryCommand)
    {
        var (vehicleId, period, stationId) = queryCommand;

        var vehicle = await vehicles.FindById(vehicleId) ?? throw new ApplicationException($"Vehicle with id {vehicleId} not found");
        var similarVehicles = await vehicles.WhichAreAvailable(period, stationId, vehicle.Category);
       

        return new ([..similarVehicles.Where(v => v.Id != vehicleId).ToData()], [..similarVehicles.ToData()]);
    }

    public async Task<VehicleData> HandleQueryAsync(ShowVehiclesCommand command)
    {
        return (await vehicles.FindById(command.VehicleId)).ToData();
    }
}