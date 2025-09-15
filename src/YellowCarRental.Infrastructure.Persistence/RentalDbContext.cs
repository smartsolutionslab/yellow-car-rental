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
        modelBuilder.Entity<Vehicle>(vehicle =>
        {
            vehicle.HasKey(v => v.Id);
            vehicle.Property<VehicleIdentifier>(v => v.Id)
                .IsRequired()
                .HasConversion(id => id.Value, id => VehicleIdentifier.Of(id));

            vehicle.Property<VehicleName>(v => v.Name)
                .IsRequired()
                .HasConversion(name => name.Value, name => new VehicleName(name));
            vehicle.Property<VehicleCategory>(v => v.Category)
                .IsRequired()
                .HasMaxLength(5)
                .HasConversion(c => c.Key, c => VehicleCategory.FromKey(c));
            vehicle.Property<FuelType>(v => v.Fuel)
                .IsRequired()
                .HasMaxLength(5)
                .HasConversion(f => f.Key, f => FuelType.FromKey(f));
            vehicle.Property<TransmissionType>(v => v.Transmission)
                .IsRequired()
                .HasMaxLength(5)
                .HasConversion(t => t.Key, t => TransmissionType.FromKey(t));
            vehicle.OwnsOne<Money>(v => v.PricePerDay, p =>
            {
                p.Property<Decimal>(p => p.Amount)
                    .IsRequired();
                p.Property<String>(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(5);
            });
            vehicle.Property<StationIdentifier>(v => v.StationId)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id));

        });

        modelBuilder.Entity<Booking>(booking =>
        {
            booking.HasKey(b => b.Id);
            booking.Property<BookingIdentifier>(b => b.Id)
                .IsRequired()
                .HasConversion(id => id.Value, id => BookingIdentifier.Of(id));
            booking.OwnsOne<BookingCustomer>(b => b.Customer, bookingCustomer =>
            {
                bookingCustomer.Property<CustomerIdentifier>(c => c.Id)
                    .IsRequired()
                    .HasConversion(id => id.Value, id => CustomerIdentifier.Of(id));
                bookingCustomer.OwnsOne<CustomerName>(c => c.Name, n =>
                {
                    n.Property<Salutation>(n => n.Salutation)
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasConversion(s => s.Value, s => Salutation.From(s));
                    n.Property<FirstName>(n => n.FirstName)
                        .IsRequired()
                        .HasConversion(name => name.Value, name => FirstName.From(name));
                    n.Property<LastName>(n => n.LastName)
                        .IsRequired()
                        .HasConversion(name => name.Value, name => LastName.From(name));
                });
                bookingCustomer.Property<BirthDate>(c => c.BirthDate)
                    .IsRequired()
                    .HasConversion(b => b.Value, b => BirthDate.From(b));
            });
            booking.Property<VehicleIdentifier>(b => b.VehicleId)
                .IsRequired()
                .HasConversion(id => id.Value, id => VehicleIdentifier.Of(id));
            booking.Property<StationIdentifier>(b => b.PickupStationId)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
            booking.Property<StationIdentifier>(b => b.ReturnStationId)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
            booking.OwnsOne<DateRange>(b => b.Period, p =>
            {
                p.Property(p => p.Start).IsRequired();
                p.Property(p => p.End).IsRequired();
            });

            booking.OwnsOne<Money>(b => b.TotalPrice, p =>
            {
                p.Property(p => p.Amount)
                    .IsRequired();
                p.Property(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(5);
                
            });
            booking.Property<BookingStatus>(b => b.Status).IsRequired();
        });

        modelBuilder.Entity<Station>(station =>
        {
            station.HasKey(s => s.Id);
            station.Property(s => s.Id)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
            station.Property<StationName>(s => s.Name)
                .IsRequired()
                .HasConversion(name => name.Value, name => StationName.From(name));
            station.OwnsOne<StationAddress>(s => s.Address, a =>
            {
                a.Property<AddressStreet>(a => a.Street)
                    .IsRequired()
                    .HasConversion(s => s.Value, s => AddressStreet.From(s));
                /*a.Property(a => a.HouseNumber)
                    .IsRequired()
                    .HasConversion(h => h.Value, h => HouseNumber.From(h));*/
                a.Property<ZipCode>(a => a.ZipCode)
                    .IsRequired()
                    .HasConversion(z => z.Value, z => ZipCode.From(z));
                a.Property<City>(a => a.City)
                    .IsRequired()
                    .HasConversion(c => c.Value, c => City.From(c));
            });
            
            station.Ignore(e => e.CurrentVehicleIds);
            station.Ignore("_vehicleAssignments");
            /*e.OwnsMany<Station.VehicleAssignment>("_vehicleAssignments", a =>
            {
                a.WithOwner().HasForeignKey(a => a.StationId).HasPrincipalKey(a => a.Id);
                a.HasKey("_id");
                a.Property<Guid>("_id")
                    .IsRequired();
                a.Property(a => a.StationId)
                    .IsRequired()
                    .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
                a.Property(a => a.VehicleId)
                    .HasConversion(id => id.Value, id => VehicleIdentifier.Of(id));
                a.HasOne(a => a.VehicleId);

            });*/
        });

        modelBuilder.Entity<Customer>(customer =>
        {
            customer.HasKey(c => c.Id);
            customer.Property<CustomerIdentifier>(c => c.Id)
                .IsRequired()
                .HasConversion(id => id.Value, id => CustomerIdentifier.Of(id));
            customer.OwnsOne<CustomerName>(c => c.Name, n =>
            {
                n.Property<Salutation>(n => n.Salutation)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasConversion(s => s.Value, s => Salutation.From(s));
                n.Property<FirstName>(n => n.FirstName)
                    .IsRequired()
                    .HasConversion(name => name.Value, name => FirstName.From(name));
                n.Property<LastName>(n => n.LastName)
                    .IsRequired()
                    .HasConversion(name => name.Value, name => LastName.From(name));
            });
            customer.Property<BirthDate>(c => c.BirthDate)
                .HasConversion(b => b.Value, b => BirthDate.From(b));
            customer.OwnsOne<CustomerAddress>(c => c.Address, a =>
            {
                a.Property<AddressStreet>(a => a.Street)
                    .IsRequired()
                    .HasConversion(s => s.Value, s => AddressStreet.From(s));
                a.Property<HouseNumber>(a => a.HouseNumber)
                    .IsRequired()
                    .HasConversion(h => h.Value, h => HouseNumber.From(h));
                a.Property<ZipCode>(a => a.ZipCode)
                    .IsRequired()
                    .HasConversion(z => z.Value, z => ZipCode.From(z));
                a.Property<City>(a => a.City)
                    .IsRequired()
                    .HasConversion(c => c.Value, c => City.From(c));
            });
            customer.Property<EMail>(c => c.EMail)
                .HasConversion(e => e.Value, e => new EMail(e));
        });
    }
}