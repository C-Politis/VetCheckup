using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VetNamesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Vet",
                newName: "MiddleName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Vet",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Vet",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "Vet",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Vet",
                type: "varchar(5)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Vet");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Vet");

            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "Vet");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Vet");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Vet",
                newName: "Name");
        }
    }
}
