using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class VetConfiguration : IEntityTypeConfiguration<Vet>
{

    #region Methods

    public void Configure(EntityTypeBuilder<Vet> builder)
    {
        builder.ToTable(nameof(Vet));

        builder.HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<Address>("AddressId")
            .IsRequired();

        builder.HasOne(e => e.ContactDetails)
            .WithOne()
            .HasForeignKey<Contact>("ContactId")
            .IsRequired();

        builder.Property(e => e.DateOfBirth)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.VetId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasKey(e => e.VetId);
    }

    #endregion

}
