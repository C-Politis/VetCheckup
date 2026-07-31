using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorOrganisationManagerRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationManager_Organisation_OrganisationId",
                table: "OrganisationManager");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationManager_OrganisationId",
                table: "OrganisationManager");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "OrganisationManager");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganisationId",
                table: "OrganisationManager",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationManager_OrganisationId",
                table: "OrganisationManager",
                column: "OrganisationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationManager_Organisation_OrganisationId",
                table: "OrganisationManager",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "OrganisationId");
        }
    }
}
