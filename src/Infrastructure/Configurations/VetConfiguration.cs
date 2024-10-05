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
            .IsRequired();

        builder.HasOne(e => e.ContactDetails)
            .WithOne()
            .IsRequired();

        builder.Property(e => e.DateOfBirth)
            .HasColumnType("datetime2");

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.VetId)
            .IsRequired();

        builder.HasKey(e => e.VetId);
    }

    #endregion

}
