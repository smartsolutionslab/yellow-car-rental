using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Vehicles : IVehicles
{
    public Task<IList<Vehicle>> With(DateRange period, StationIdentifier? stationId = null, VehicleCategory? category = null)
    {
        throw new NotImplementedException();
    }

    public Task<Vehicle> FindById(VehicleIdentifier vehicleId)
    {
        throw new NotImplementedException();
    }
}

public class Bookings : IBookings
{
    public Task<IList<Booking>> All()
    {
        throw new NotImplementedException();
    }

    public Task<IList<Booking>> With(DateRange period, StationIdentifier? stationId, CustomerIdentifier? customerId)
    {
        throw new NotImplementedException();
    }

    public Task<Booking> FindById(BookingIdentifier bookingId)
    {
        throw new NotImplementedException();
    }

    public Task Add(Booking booking)
    {
        throw new NotImplementedException();
    }
}

public class Customers : ICustomers
{
    public Task<IList<Customer>> All()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> FindById(CustomerIdentifier customerId)
    {
        throw new NotImplementedException();
    }

    public Task Add(Customer customer)
    {
        throw new NotImplementedException();
    }
}


public static class Extensions
{
    public static TBuilder AddPersistence<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddScoped<IVehicles, Vehicles>();
        builder.Services.AddScoped<IStations, Stations>();
        builder.Services.AddScoped<IBookings, Bookings>();

        return builder;
    }
}