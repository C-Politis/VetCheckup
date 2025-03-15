using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{

    #region Methods

    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable(nameof(Owner));

        builder.HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<Address>("AddressId")
            .IsRequired();

        builder.HasOne(e => e.ContactDetails)
            .WithOne()
            .HasForeignKey<Contact>("ContactId")
            .IsRequired();

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

        builder.Property(e => e.OwnerId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasKey(e => e.OwnerId);
    }

    #endregion

}
