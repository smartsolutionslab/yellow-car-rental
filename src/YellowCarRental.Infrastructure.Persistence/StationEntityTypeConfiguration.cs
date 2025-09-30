using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class StationEntityTypeConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .IsRequired()
            .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
        builder.Property<StationName>(s => s.Name)
            .IsRequired()
            .HasConversion(name => name.Value, name => StationName.From(name));
        builder.OwnsOne<StationAddress>(s => s.Address, a =>
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
            
        builder.Ignore(e => e.CurrentVehicleIds);
        builder.Ignore("_vehicleAssignments");
        /*e.OwnsMany<Station.VehicleAssignment>("_vehicleAssignments", a =>
        {
            a.WithOwner().HasForeignKey("StationId");
            a.Property<Guid>("Id");
            a.HasKey("Id");
            a.Property(a => a.StationId)
                .IsRequired()
                .HasConversion(id => id.Value, id => StationIdentifier.Of(id)); 
            a.Property(a => a.VehicleId)
                .IsRequired()
                .HasConversion(id => id.Value, id => VehicleIdentifier.Of(id));
            });
        
  */      

    }
}