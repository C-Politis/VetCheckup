using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureAddressAndContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Vet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Vet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Owner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Owner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Organisation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Organisation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vet_AddressId",
                table: "Vet",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vet_ContactId",
                table: "Vet",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owner_AddressId",
                table: "Owner",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owner_ContactId",
                table: "Owner",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_AddressId",
                table: "Organisation",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_ContactId",
                table: "Organisation",
                column: "ContactId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organisation_Address_AddressId",
                table: "Organisation",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organisation_Contact_ContactId",
                table: "Organisation",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_Address_AddressId",
                table: "Owner",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_Contact_ContactId",
                table: "Owner",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vet_Address_AddressId",
                table: "Vet",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vet_Contact_ContactId",
                table: "Vet",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisation_Address_AddressId",
                table: "Organisation");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisation_Contact_ContactId",
                table: "Organisation");

            migrationBuilder.DropForeignKey(
                name: "FK_Owner_Address_AddressId",
                table: "Owner");

            migrationBuilder.DropForeignKey(
                name: "FK_Owner_Contact_ContactId",
                table: "Owner");

            migrationBuilder.DropForeignKey(
                name: "FK_Vet_Address_AddressId",
                table: "Vet");

            migrationBuilder.DropForeignKey(
                name: "FK_Vet_Contact_ContactId",
                table: "Vet");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Vet_AddressId",
                table: "Vet");

            migrationBuilder.DropIndex(
                name: "IX_Vet_ContactId",
                table: "Vet");

            migrationBuilder.DropIndex(
                name: "IX_Owner_AddressId",
                table: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_Owner_ContactId",
                table: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_Organisation_AddressId",
                table: "Organisation");

            migrationBuilder.DropIndex(
                name: "IX_Organisation_ContactId",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Vet");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Vet");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Organisation");
        }
    }
}
