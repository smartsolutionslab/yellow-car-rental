using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public static class Extensions
{
    public static TBuilder AddPersistence<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddDbContext<RentalDbContext>(options =>
            //options.UseSqlite($"Data Source=yellow-car-rental.db"));
            options.UseInMemoryDatabase("yellow-car-rentel-db").EnableDetailedErrors().EnableSensitiveDataLogging());
            
        
        builder.Services.AddScoped<ICustomers, Customers>();
        builder.Services.AddScoped<IVehicles, Vehicles>();
        builder.Services.AddScoped<IStations, Stations>();
        builder.Services.AddScoped<IBookings, Bookings>();
        return builder;
    }

    public static IServiceProvider UsePersistence(this IServiceProvider services, IConfiguration configuration)
    {
        var scope = services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        RentalDbContext dbContext = scopedServices.GetRequiredService<RentalDbContext>();
        dbContext.Database.EnsureCreated();

        return services;
    }
}