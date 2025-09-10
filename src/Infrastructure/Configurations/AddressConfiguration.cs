using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Infrastructure.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {

        #region Methods

        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.Property(e => e.AddressId)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.PostalCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.StreetAddress)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Suburb)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasKey(e => e.AddressId);
        }

        #endregion

    }
}
