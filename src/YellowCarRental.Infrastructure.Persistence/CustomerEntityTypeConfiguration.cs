using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property<CustomerIdentifier>(c => c.Id)
            .IsRequired()
            .HasConversion(id => id.Value, id => CustomerIdentifier.Of(id));
        builder.OwnsOne<CustomerName>(c => c.Name, n =>
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
        builder.Property<BirthDate>(c => c.BirthDate)
            .HasConversion(b => b.Value, b => BirthDate.From(b));
        builder.OwnsOne<CustomerAddress>(c => c.Address, a =>
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
        builder.Property<EMail>(c => c.EMail)
            .HasConversion(e => e.Value, e => new EMail(e));
    }
}