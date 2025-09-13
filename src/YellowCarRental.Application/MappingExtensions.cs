using SmartSolutionsLab.YellowCarRental.Api.Contracts;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public static class MappingExtensions
{
    public static IEnumerable<VehicleData> ToData(this IEnumerable<Vehicle> vehicles)
    {
        return vehicles.Select(v => new VehicleData
        {
            Name = v.Name.Value,
            Category = v.Category.Key,
            Seats = v.Seats,
            Fuel = v.Fuel.Key,
            Transmission = v.Transmission.Key,
            PricePerDay = v.PricePerDay.Amount,
        });
    }
}