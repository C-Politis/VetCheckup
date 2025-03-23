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
        
        builder.HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<Address>("AddressId")
            .IsRequired();

        builder.HasOne(e => e.ContactDetails)
            .WithOne()
            .HasForeignKey<Contact>("ContactId")
            .IsRequired();

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
