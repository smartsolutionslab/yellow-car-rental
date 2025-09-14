using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public static class Extensions
{
    public static TBuilder AddPersistence<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddScoped<ICustomers, Customers>();
        builder.Services.AddScoped<IVehicles, Vehicles>();
        builder.Services.AddScoped<IStations, Stations>();
        builder.Services.AddScoped<IBookings, Bookings>();

        return builder;
    }
}