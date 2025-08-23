using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Infrastructure.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(e => e.ContactId)
                .IsRequired();
            
            builder.Property(e => e.Email)
                .IsRequired();
            
            builder.Property(e => e.Mobile)
                .IsRequired();

            builder.HasKey(e => e.ContactId);
        }
    }
}
