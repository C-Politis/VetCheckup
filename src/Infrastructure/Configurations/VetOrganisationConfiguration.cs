using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class VetOrganisationConfiguration : IEntityTypeConfiguration<VetOrganisation>
{
    #region Methods

    public void Configure(EntityTypeBuilder<VetOrganisation> builder)
    {
        builder.ToTable(nameof(VetOrganisation));

        builder.HasKey(e => new { e.VetId, e.OrganisationId });

        builder.HasOne(e => e.Vet)
            .WithMany(v => v.VetOrganisations)
            .HasForeignKey(e => e.VetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Organisation)
            .WithMany(o => o.VetOrganisations)
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    #endregion
}
