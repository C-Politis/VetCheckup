using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetCheckup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResolveOwnerShadowKeyForPet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Owner_OwnerId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Owner_OwnerId1",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Pet_OwnerId1",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Pet");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Pet",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Owner_OwnerId",
                table: "Pet",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Owner_PetId",
                table: "Pet",
                column: "PetId",
                principalTable: "Owner",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Owner_OwnerId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Owner_PetId",
                table: "Pet");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Pet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
                name: "FK_Pet_Owner_OwnerId",
                table: "Pet",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Owner_OwnerId1",
                table: "Pet",
                column: "OwnerId1",
                principalTable: "Owner",
                principalColumn: "OwnerId");
        }
    }
}
