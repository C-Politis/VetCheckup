using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{

    #region Methods

    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable(nameof(Pet));

        builder.Property(e => e.DateOfBirth)
            .HasColumnType("datetime2");

        builder.Property(e => e.MicrochipId);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.PetId)
            .IsRequired();

        builder.Property(e => e.PetId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Sex)
            .HasConversion(propVal => (int)propVal, dbVal => (Sex)dbVal)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(e => e.Species)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasKey(e => e.PetId);
    }

    #endregion

}
