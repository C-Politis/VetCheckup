using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{

    #region Methods

    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(nameof(Address));

        builder.Property(e => e.AddressId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Country)
            .HasMaxLength(100);

        builder.Property(e => e.PostalCode)
            .HasMaxLength(20);

        builder.Property(e => e.State)
            .HasMaxLength(100);

        builder.Property(e => e.StreetAddress)
            .HasMaxLength(250);

        builder.Property(e => e.Suburb)
            .HasMaxLength(100);

        builder.HasKey(e => e.AddressId);
    }

    #endregion

}
