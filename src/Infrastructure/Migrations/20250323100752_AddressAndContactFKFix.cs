using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddressAndContactFKFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Owner_AddressId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Vet_AddressId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Owner_ContactId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Vet_ContactId",
                table: "Contact");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_Address_AddressId",
                table: "Owner",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_Contact_ContactId",
                table: "Owner",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vet_Address_AddressId",
                table: "Vet",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vet_Contact_ContactId",
                table: "Vet",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Owner_AddressId",
                table: "Address",
                column: "AddressId",
                principalTable: "Owner",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Vet_AddressId",
                table: "Address",
                column: "AddressId",
                principalTable: "Vet",
                principalColumn: "VetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Owner_ContactId",
                table: "Contact",
                column: "ContactId",
                principalTable: "Owner",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Vet_ContactId",
                table: "Contact",
                column: "ContactId",
                principalTable: "Vet",
                principalColumn: "VetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
