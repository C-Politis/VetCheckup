using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;
public class VetPetConfiguration : IEntityTypeConfiguration<VetPet>
{

    #region Methods

    public void Configure(EntityTypeBuilder<VetPet> builder)
    {
        builder.ToTable(nameof(VetPet));

        builder.HasOne(e => e.Pet)
            .WithMany()
            .HasForeignKey("PetId")
            .IsRequired();

        builder.HasOne(e => e.Vet)
            .WithMany()
            .HasForeignKey("VetId")
            .IsRequired();

        builder.HasKey("VetId", "PetId");
    }

    #endregion

}
