using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friends.Net.Migrations
{
    /// <inheritdoc />
    public partial class ldapusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConnectedToLdap",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectedToLdap",
                table: "AspNetUsers");
        }
    }
}
