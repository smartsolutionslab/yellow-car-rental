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
        optionsBuilder.UseAsyncSeeding(async (dbContext, options, cancellationToken) =>
        {
            var customers = Seed.CustomerTestData.GetAll(50);
            // for demo purposes only
            var demoCustomerId =CustomerIdentifier.Of("8f5d9c4a-2b3e-4f1a-9d6e-7c8b2a1f3e45");
        
            customers.First().OverrideId(demoCustomerId);

            var vehicles = VehicleInitData.AllVehicles.ToList();
            var stations = StationsInitData.AllStations.ToList();
            
            await dbContext.AddRangeAsync(customers, cancellationToken);
            await dbContext.AddRangeAsync(vehicles, cancellationToken);
            await dbContext.AddRangeAsync(stations, cancellationToken);
            await dbContext.AddRangeAsync(BookingSeedData.GetAll(
                                                  customers,
                                                  stations,
                                                  vehicles.Select(v => v.Id).ToList(),
                                                  250).ToList(),
                cancellationToken);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(e =>
        {
            e.HasKey(v => v.Id);
            e.Property(v => v.Id)
                .IsRequired()
                .HasConversion(id => id.Value, id => VehicleIdentifier.Of(id));

            e.Property(v => v.Name)
                .IsRequired()
                .HasConversion(name => name.Value, name => new VehicleName(name));
            e.Property(v => v.Category)
                .IsRequired()
                .HasMaxLength(5)
                .HasConversion(c => c.Key, c => VehicleCategory.FromKey(c));
            e.Property(v => v.Fuel)
                .IsRequired()
                .HasMaxLength(5)
                .HasConversion(f => f.Key, f => FuelType.FromKey(f));
            e.Property(v => v.Transmission)
                .IsRequired()
                .HasMaxLength(5)
                .HasConversion(t => t.Key, t => TransmissionType.FromKey(t));
            e.ComplexProperty(v => v.PricePerDay, p =>
            {
                p.Property(p => p.Amount).IsRequired();
                p.Property(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(5);
            });
            e.Property(v => v.StationId)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id));

        });

        modelBuilder.Entity<Booking>(e =>
        {
            e.HasKey(b => b.Id);
            e.Property(b => b.Id)
                .IsRequired()
                .HasConversion(id => id.Value, id => BookingIdentifier.Of(id));

            e.ComplexProperty(b => b.Customer, c =>
            {
                c.Property(c => c.Id)
                    .IsRequired()
                    .HasConversion(id => id.Value, id => CustomerIdentifier.Of(id));
                c.ComplexProperty(c => c.Name, n =>
                {
                    n.Property(n => n.Salutation)
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasConversion(s => s.Value, s => Salutation.From(s));
                    n.Property(n => n.FirstName)
                        .IsRequired()
                        .HasConversion(name => name.Value, name => FirstName.From(name));
                    n.Property(n => n.LastName)
                        .IsRequired()
                        .HasConversion(name => name.Value, name => LastName.From(name));
                });
                c.Property(c => c.BirthDate)
                    .HasConversion(b => b.Value, b => BirthDate.From(b));
            });
            e.Property(b => b.PickupStationId)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
            e.Property(b => b.ReturnStationId)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
            e.ComplexProperty(b => b.Period)
                .IsRequired();
            e.ComplexProperty(b => b.TotalPrice, p =>
            {
                p.Property(p => p.Amount).IsRequired();
                p.Property(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(5);
            });
            e.Property(b => b.Status).IsRequired();
        });

        modelBuilder.Entity<Station>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Id)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
            e.Property(s => s.Name)
                .IsRequired()
                .HasConversion(name => name.Value, name => StationName.From(name));
        });

        modelBuilder.Entity<Customer>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id)
                .IsRequired()
                .HasConversion(id => id.Value, id => CustomerIdentifier.Of(id));
            e.ComplexProperty(c => c.Name, n =>
            {
                n.Property(n => n.Salutation)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasConversion(s => s.Value, s => Salutation.From(s));
                n.Property(n => n.FirstName)
                    .IsRequired()
                    .HasConversion(name => name.Value, name => FirstName.From(name));
                n.Property(n => n.LastName)
                    .IsRequired()
                    .HasConversion(name => name.Value, name => LastName.From(name));
            });
            e.Property(c => c.BirthDate)
                .HasConversion(b => b.Value, b => BirthDate.From(b));
            e.ComplexProperty(c => c.Address, a =>
            {
                a.Property(a => a.Street)
                    .IsRequired()
                    .HasConversion(s => s.Value, s => AddressStreet.From(s));
                a.Property(a => a.HouseNumber)
                    .IsRequired()
                    .HasConversion(h => h.Value, h => HouseNumber.From(h));
                a.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasConversion(z => z.Value, z => ZipCode.From(z));
                a.Property(a => a.City)
                    .IsRequired()
                    .HasConversion(c => c.Value, c => City.From(c));
            });
            e.Property(c => c.EMail)
                .HasConversion(e => e.Value, e => new EMail(e));


        });
    }
}