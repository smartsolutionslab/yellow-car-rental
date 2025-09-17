using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property<VehicleIdentifier>(v => v.Id)
            .IsRequired()
            .HasConversion(id => id.Value, id => VehicleIdentifier.Of(id));

        builder.Property<VehicleName>(v => v.Name)
            .IsRequired()
            .HasConversion(name => name.Value, name => new VehicleName(name));
        builder.Property<VehicleCategory>(v => v.Category)
            .IsRequired()
            .HasMaxLength(5)
            .HasConversion(c => c.Key, c => VehicleCategory.FromKey(c));
        builder.Property<FuelType>(v => v.Fuel)
            .IsRequired()
            .HasMaxLength(5)
            .HasConversion(f => f.Key, f => FuelType.FromKey(f));
        builder.Property<TransmissionType>(v => v.Transmission)
            .IsRequired()
            .HasMaxLength(5)
            .HasConversion(t => t.Key, t => TransmissionType.FromKey(t));
        builder.OwnsOne<Money>(v => v.PricePerDay, p =>
        {
            p.Property<Decimal>(p => p.Amount)
                .IsRequired();
            p.Property<String>(p => p.Currency)
                .IsRequired()
                .HasMaxLength(5);
        });
        builder.Property<StationIdentifier>(v => v.StationId)
            .IsRequired()
            .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
    }
}