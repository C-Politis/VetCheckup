using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;
public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{

    #region Methods

    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable(nameof(Owner));

        builder.OwnsOne(e => e.Address)
            .WithOwner();

        builder.OwnsOne(e => e.ContactDetails)
            .WithOwner();

        builder.Property(e => e.DateOfBirth)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.OwnerId)
            .IsRequired();

        builder.HasKey(e => e.OwnerId);
    }

    #endregion

}
