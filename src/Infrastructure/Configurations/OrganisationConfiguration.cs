using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Infrastructure.Configurations;

public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
{
    
    #region Methods

    public void Configure(EntityTypeBuilder<Organisation> builder)
    {
        builder.ToTable(nameof(Organisation));
        
        builder.Property(e => e.Abn)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property<Guid>("AddressId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<Organisation>("AddressId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property<Guid>("ContactId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(e => e.ContactDetails)
            .WithOne()
            .HasForeignKey<Organisation>("ContactId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.OrganisationType)
            .HasConversion(propVal => (int)propVal, dbVal => (OrganisationType)dbVal)
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(e => e.OrganisationId)
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.HasKey(e => e.OrganisationId);
    }

    #endregion
    
}
