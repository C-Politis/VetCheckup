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
            .IsRequired();

        builder.HasOne(e => e.Vet)
            .WithMany()
            .IsRequired();

        builder.HasKey("VetId", "PetId");
    }

    #endregion

}
