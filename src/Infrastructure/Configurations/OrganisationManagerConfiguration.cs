using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class OrganisationManagerConfiguration : IEntityTypeConfiguration<OrganisationManager>
{
    public void Configure(EntityTypeBuilder<OrganisationManager> builder)
    {
        
        builder.ToTable(nameof(OrganisationManager));

        builder.HasKey(e => e.OrganisationManagerId);

        builder.Property(e => e.OrganisationManagerId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<OrganisationManager>()            
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ContactDetails)
            .WithOne()
            .HasForeignKey<OrganisationManager>()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
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
        
        builder.HasOne(e => e.Organisation)
            .WithOne()
            .HasForeignKey("OrganisationId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            
    }
}
