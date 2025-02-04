using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friends.Net.Migrations
{
    /// <inheritdoc />
    public partial class AddIconName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "AppShortcuts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "AppShortcuts");
        }
    }
}
