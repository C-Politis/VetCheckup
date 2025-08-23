using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class VetConfiguration : IEntityTypeConfiguration<Vet>
{

    #region Methods

    public void Configure(EntityTypeBuilder<Vet> builder)
    {
        builder.ToTable(nameof(Vet));

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
