using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSolutionsLab.YellowCarRental.Api.Contracts;

namespace SmartSolutionsLab.YellowCarRental.Application;

public static class RegisterCommandHandlers
{
    public static TBuilder RegisterAllApplicationCommandsAndHandlers<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddScoped<IQueryCommandHandler<SearchVehiclesQueryCommand, SearchVehiclesQueryResult>, VehicleCommandHandlers>();

        return builder;
    }
}