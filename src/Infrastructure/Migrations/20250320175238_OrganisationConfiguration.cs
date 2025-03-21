using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrganisationConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    OrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Abn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganisationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.OrganisationId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Organisation_AddressId",
                table: "Address",
                column: "AddressId",
                principalTable: "Organisation",
                principalColumn: "OrganisationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Organisation_ContactId",
                table: "Contact",
                column: "ContactId",
                principalTable: "Organisation",
                principalColumn: "OrganisationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Organisation_AddressId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Organisation_ContactId",
                table: "Contact");

            migrationBuilder.DropTable(
                name: "Organisation");
        }
    }
}
