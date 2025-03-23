using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetCheckup.Infrastructure.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{

    #region Methods

    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable(nameof(Contact));

        builder.Property(e => e.ContactId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Mobile)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasKey(e => e.ContactId);
    }

    #endregion

}
