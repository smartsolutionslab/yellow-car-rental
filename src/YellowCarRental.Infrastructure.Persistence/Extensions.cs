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
        builder.AddOracleDatabaseDbContext<RentalDbContext>(
            connectionName: "carrentaldb",
            null,
            optionsBuilder =>
                optionsBuilder.UseSeeding((dbContext, seed) =>
                {
                    if (seed == false) return;

                    var customers = Seed.CustomerTestData.GetAll(50);
                    // for demo purposes only
                    var demoCustomerId = CustomerIdentifier.Of("8f5d9c4a-2b3e-4f1a-9d6e-7c8b2a1f3e45");

                    customers.First().OverrideId(demoCustomerId);

                    var vehicles = VehicleInitData.AllVehicles.ToList();
                    var stations = StationsInitData.AllStations.ToList();

                    dbContext.AddRange(customers);
                    dbContext.AddRange(vehicles);
                    dbContext.AddRange(stations);
                    dbContext.AddRange(BookingSeedData.GetAll(
                        customers,
                        stations,
                        vehicles.Select(v => v.Id).ToList(),
                        250)
                        .ToList());

                    dbContext.SaveChanges();
                }));
            
            
            
        
        //builder.Services.AddDbContext<RentalDbContext>(options =>
            //options.UseOracle(options => options.)
            //options.UseSqlite($"Data Source=yellow-car-rental.db"));
            //options.UseInMemoryDatabase("yellow-car-rentel-db").EnableDetailedErrors().EnableSensitiveDataLogging());
            
            
        
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