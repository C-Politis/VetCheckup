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
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.Property(e => e.AddressId)
                .IsRequired();

            builder.Property(e => e.Country)
                .IsRequired();

            builder.Property(e => e.PostalCode)
                .IsRequired();

            builder.Property(e => e.State)
                .IsRequired();

            builder.Property(e => e.StreetAddress)
                .IsRequired();

            builder.Property(e => e.Suburb)
                .IsRequired();

            builder.HasKey(e => e.AddressId);
        }
    }
}
