using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public static class RegisterCommandHandlers
{
    public static TBuilder RegisterAllApplicationCommandsAndHandlers<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        // Vehicle handlers
        builder.Services.AddScoped<IQueryCommandHandler<SearchVehiclesQueryCommand, SearchVehiclesQueryResult>, VehicleCommandHandlers>();
        
        // Vehicle handlers
        builder.Services.AddScoped<IQueryCommandHandler<ListAllStationsQueryCommand, ListStationsQueryResult>, StationsCommandHandlers>();
        
        // Booking handlers
        builder.Services.AddScoped<IQueryCommandHandler<ListAllBookingsQueryCommand, SearchBookingsQueryResult>, BookingCommandHandlers>();
        builder.Services.AddScoped<IQueryCommandHandler<SearchBookingsQueryCommand, SearchBookingsQueryResult>, BookingCommandHandlers>();
        builder.Services.AddScoped<IQueryCommandHandler<CheckBookingAvailabilityQueryCommand, SearchBookingsQueryResult>, BookingCommandHandlers>();
        
        builder.Services.AddScoped<ICommandHandler<BookVehicleCommand, BookingIdentifier>, BookingCommandHandlers>();
        builder.Services.AddScoped<ICommandHandler<CancelBookingCommand, BookingIdentifier>, BookingCommandHandlers>();
        
        // Customer handlers
        builder.Services.AddScoped<ICommandHandler<RegisterCustomerCommand, CustomerIdentifier>, CustomerCommandHandlers>();
        builder.Services.AddScoped<IQueryCommandHandler<ShowAllCustomersCommand, ListCustomersQueryResult>, CustomerCommandHandlers>();
        builder.Services.AddScoped<IQueryCommandHandler<ShowCustomerCommand, CustomerData>, CustomerCommandHandlers>();

        return builder;
    }
}