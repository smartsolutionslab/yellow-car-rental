using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;



public class RentalDbContext(DbContextOptions<RentalDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
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
                250).ToList());

            dbContext.SaveChanges();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new VehicleEntityTypeConfiguration().Configure(modelBuilder.Entity<Vehicle>());
        new StationEntityTypeConfiguration().Configure(modelBuilder.Entity<Station>());
        new CustomerEntityTypeConfiguration().Configure(modelBuilder.Entity<Customer>());
        new BookingEntityTypeConfiguration().Configure(modelBuilder.Entity<Booking>());
    }
}