using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class OrganisationManagerConfiguration : IEntityTypeConfiguration<OrganisationManager>
{
    public void Configure(EntityTypeBuilder<OrganisationManager> builder)
    {
        builder.ToTable(nameof(OrganisationManager));

        // Primary Key
        builder.HasKey(e => e.OrganisationManagerId);

        builder.Property(e => e.OrganisationManagerId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Address relationship
        builder.Property<Guid>("AddressId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<OrganisationManager>("AddressId")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        // Contact relationship
        builder.Property<Guid>("ContactId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(e => e.ContactDetails)
            .WithOne()
            .HasForeignKey<OrganisationManager>("ContactId")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        // Organisation relationship
        builder.Property<Guid>("OrganisationId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey("OrganisationId")
            .IsRequired();

        // Personal details
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
    }
}
