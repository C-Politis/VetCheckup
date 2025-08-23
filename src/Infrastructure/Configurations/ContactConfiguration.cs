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
    }
}
