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
            .HasForeignKey<Vet>("AddressId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.ContactDetails)
            .WithOne()
            .HasForeignKey<Vet>("ContactId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.DateOfBirth)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasColumnType("varchar(5)");

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.MiddleName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Suffix)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder.Property(e => e.VetId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasKey(e => e.VetId);
    }

    #endregion

}
