using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class VetOrganisationConfiguration : IEntityTypeConfiguration<VetOrganisation>
{
    #region Methods

    public void Configure(EntityTypeBuilder<VetOrganisation> builder)
    {
        builder.ToTable(nameof(VetOrganisation));

        builder.HasKey("VetId", "OrganisationId");

        builder.HasOne(vo => vo.Vet)
            .WithMany(v => v.VetOrganisations)
            .HasForeignKey("VetId");
        
        builder.HasOne(vo => vo.Organisation)
            .WithMany(o => o.VetOrganisations)
            .HasForeignKey("OrganisationId");
        
        builder.Property(vo => vo.IsPrimaryOrganisation)
            .IsRequired();
    }
    
    #endregion
}
