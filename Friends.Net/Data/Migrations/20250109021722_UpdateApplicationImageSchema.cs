using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friends.Net.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationImageSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "AppShortcuts",
                newName: "IconId");

            migrationBuilder.CreateTable(
                name: "AppImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppShortcuts_IconId",
                table: "AppShortcuts",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppShortcuts_AppImages_IconId",
                table: "AppShortcuts",
                column: "IconId",
                principalTable: "AppImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppShortcuts_AppImages_IconId",
                table: "AppShortcuts");

            migrationBuilder.DropTable(
                name: "AppImages");

            migrationBuilder.DropIndex(
                name: "IX_AppShortcuts_IconId",
                table: "AppShortcuts");

            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "AppShortcuts",
                newName: "Icon");
        }
    }
}
