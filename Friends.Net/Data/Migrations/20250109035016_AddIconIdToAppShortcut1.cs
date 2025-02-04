using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friends.Net.Migrations
{
    /// <inheritdoc />
    public partial class AddIconIdToAppShortcut1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "AppImages",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "AppImages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64",
                table: "AppImages");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppImages",
                newName: "Url");
        }
    }
}
