using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Vet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Owner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "OrganisationManager",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vet_UserId",
                table: "Vet",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owner_UserId",
                table: "Owner",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationManager_UserId",
                table: "OrganisationManager",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationManager_User_UserId",
                table: "OrganisationManager",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_User_UserId",
                table: "Owner",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vet_User_UserId",
                table: "Vet",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationManager_User_UserId",
                table: "OrganisationManager");

            migrationBuilder.DropForeignKey(
                name: "FK_Owner_User_UserId",
                table: "Owner");

            migrationBuilder.DropForeignKey(
                name: "FK_Vet_User_UserId",
                table: "Vet");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Vet_UserId",
                table: "Vet");

            migrationBuilder.DropIndex(
                name: "IX_Owner_UserId",
                table: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationManager_UserId",
                table: "OrganisationManager");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vet");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrganisationManager");
        }
    }
}
