using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPetListToOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId1",
                table: "Pet",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pet_OwnerId1",
                table: "Pet",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Owner_OwnerId1",
                table: "Pet",
                column: "OwnerId1",
                principalTable: "Owner",
                principalColumn: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Owner_OwnerId1",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Pet_OwnerId1",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Pet");
        }
    }
}
