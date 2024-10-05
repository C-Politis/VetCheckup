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
            .IsRequired();

        builder.Property(e => e.Email)
            .HasMaxLength(100);

        builder.Property(e => e.Mobile)
            .HasMaxLength(20);

        builder.HasKey(e => e.ContactId);
    }

    #endregion

}
