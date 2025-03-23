﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VetCheckup.Infrastructure.Data;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VetCheckup.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Suburb")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AddressId");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ContactId");

                    b.ToTable("Contact", (string)null);
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Organisation", b =>
                {
                    b.Property<Guid>("OrganisationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abn")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OrganisationType")
                        .HasColumnType("int");

                    b.HasKey("OrganisationId");

                    b.ToTable("Organisation", (string)null);
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Suffix")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.HasKey("OwnerId");

                    b.ToTable("Owner", (string)null);
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Pet", b =>
                {
                    b.Property<Guid>("PetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("MicrochipId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OwnerId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PetId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("OwnerId1");

                    b.ToTable("Pet", (string)null);
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Vet", b =>
                {
                    b.Property<Guid>("VetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("VetId");

                    b.ToTable("Vet", (string)null);
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.VetPet", b =>
                {
                    b.Property<Guid>("VetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VetId", "PetId");

                    b.HasIndex("PetId");

                    b.ToTable("VetPet", (string)null);
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Address", b =>
                {
                    b.HasOne("VetCheckup.Domain.Entities.Organisation", null)
                        .WithOne("Address")
                        .HasForeignKey("VetCheckup.Domain.Entities.Address", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetCheckup.Domain.Entities.Owner", null)
                        .WithOne("Address")
                        .HasForeignKey("VetCheckup.Domain.Entities.Address", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetCheckup.Domain.Entities.Vet", null)
                        .WithOne("Address")
                        .HasForeignKey("VetCheckup.Domain.Entities.Address", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Contact", b =>
                {
                    b.HasOne("VetCheckup.Domain.Entities.Organisation", null)
                        .WithOne("ContactDetails")
                        .HasForeignKey("VetCheckup.Domain.Entities.Contact", "ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetCheckup.Domain.Entities.Owner", null)
                        .WithOne("ContactDetails")
                        .HasForeignKey("VetCheckup.Domain.Entities.Contact", "ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetCheckup.Domain.Entities.Vet", null)
                        .WithOne("ContactDetails")
                        .HasForeignKey("VetCheckup.Domain.Entities.Contact", "ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Pet", b =>
                {
                    b.HasOne("VetCheckup.Domain.Entities.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetCheckup.Domain.Entities.Owner", null)
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId1");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.VetPet", b =>
                {
                    b.HasOne("VetCheckup.Domain.Entities.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VetCheckup.Domain.Entities.Vet", "Vet")
                        .WithMany()
                        .HasForeignKey("VetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Organisation", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("ContactDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Owner", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("ContactDetails")
                        .IsRequired();

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("VetCheckup.Domain.Entities.Vet", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("ContactDetails")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
