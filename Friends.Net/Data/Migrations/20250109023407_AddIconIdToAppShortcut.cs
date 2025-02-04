using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friends.Net.Migrations
{
    /// <inheritdoc />
    public partial class AddIconIdToAppShortcut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppShortcuts_AppImages_IconId",
                table: "AppShortcuts");

            migrationBuilder.DropIndex(
                name: "IX_AppShortcuts_IconId",
                table: "AppShortcuts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
