using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{

    #region Methods

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.Property(e => e.Password)
            .IsRequired();

        builder.Property(e => e.UserName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.UserId)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder.Property(e => e.UserType)
            .HasConversion(propVal => (int)propVal, dbVal => (UserType)dbVal)
            .HasColumnType("int")
            .IsRequired();

        builder.HasKey(e => e.UserId);
    }

    #endregion

}
