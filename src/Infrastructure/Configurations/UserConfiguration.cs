using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.Property(e => e.Role)
            .HasConversion(propVal => (int)propVal, dbVal => (Roles)dbVal)
            .HasColumnType("int")
            .IsRequired();
    }
}
