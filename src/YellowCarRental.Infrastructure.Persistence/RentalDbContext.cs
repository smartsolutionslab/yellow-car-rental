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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new VehicleEntityTypeConfiguration().Configure(modelBuilder.Entity<Vehicle>());
        new StationEntityTypeConfiguration().Configure(modelBuilder.Entity<Station>());
        new CustomerEntityTypeConfiguration().Configure(modelBuilder.Entity<Customer>());
        new BookingEntityTypeConfiguration().Configure(modelBuilder.Entity<Booking>());
    }
}