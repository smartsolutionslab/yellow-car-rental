using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property<BookingIdentifier>(b => b.Id)
            .IsRequired()
            .HasConversion(id => id.Value, id => BookingIdentifier.Of(id));
        builder.OwnsOne<BookingCustomer>(b => b.Customer, bookingCustomer =>
        {
            bookingCustomer.WithOwner();

            bookingCustomer.Property<CustomerIdentifier>(c => c.Id)
                .HasColumnName("Customer_Id")
                .IsRequired()

                .HasConversion(id => id.Value, id => CustomerIdentifier.Of(id));
            /*
            bookingCustomer.OwnsOne<CustomerName>(c => c.Name, n =>
            {
                n.Navigation(n => n.Salutation).IsRequired();
                n.Property<Salutation>(n => n.Salutation)
                    .HasColumnName("Customer_Salutation")
                    .HasConversion<string>(s => s.Value, s => new Salutation(s))
                    .IsRequired();

                n.Navigation(n => n.FirstName).IsRequired();
                n.Property<FirstName>(n => n.FirstName)
                    .HasColumnName("Customer_FirstName")
                    .HasConversion<string>(n => n.Value, n => new FirstName(n))
                    .IsRequired();

                n.Navigation(n => n.LastName).IsRequired();
                n.Property<LastName>(n => n.LastName)
                    .HasColumnName("Customer_LastName")
                    .HasConversion<string>(n => n.Value, n => new LastName(n))
                    .IsRequired();

            });*/
            bookingCustomer.Property<CustomerName>(c => c.Name)
                .HasConversion<string>(
                    name => name.Salutation.Value + "," + name.FirstName.Value + "," + name.LastName.Value,
                    name => CustomerName.Parse(name));

            bookingCustomer.Property<BirthDate>(c => c.BirthDate)
                .HasConversion(d => d.Value, d => BirthDate.From(d));
        });

        builder.Property<VehicleIdentifier>(b => b.VehicleId)
            .IsRequired()
            .HasConversion(id => id.Value, id => VehicleIdentifier.Of(id));
        builder.Property<StationIdentifier>(b => b.PickupStationId)
            .IsRequired()
            .HasConversion(id => id.Value, id => StationIdentifier.Of(id));
        builder.Property<StationIdentifier>(b => b.ReturnStationId)
            .IsRequired()
            .HasConversion(id => id.Value, id => StationIdentifier.Of(id));

        builder.OwnsOne<DateRange>(b => b.Period, p =>
        {
            p.Property(p => p.Start).IsRequired();
            p.Property(p => p.End).IsRequired();
        });

        builder.OwnsOne<Money>(b => b.TotalPrice, p =>
        {
            p.Property(p => p.Amount)
                .IsRequired();
            p.Property(p => p.Currency)
                .IsRequired()
                .HasMaxLength(5);

        });
        builder.Property<BookingStatus>(b => b.Status)
            .HasConversion<string>()
            .IsRequired();
    }
}