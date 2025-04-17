using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDbContext2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Housenumber",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Housenumber",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Addresses");
        }
    }
}
