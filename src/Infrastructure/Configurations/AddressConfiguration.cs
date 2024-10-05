using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Infrastructure.Configurations;
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{

    #region Methods

    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(nameof(Address));

        builder.Property(e => e.AddressId)
            .IsRequired();

        builder.Property(e => e.Country)
            .HasMaxLength(100);

        builder.Property(e => e.PostalCode)
            .HasMaxLength(20);

        builder.Property(e => e.State)
            .HasMaxLength(100);

        builder.Property(e => e.StreetAddress)
            .HasMaxLength(250);

        builder.Property(e => e.Suburb)
            .HasMaxLength(100);

        builder.HasKey(e => e.AddressId);  
    }

    #endregion

}
