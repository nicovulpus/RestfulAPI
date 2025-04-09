using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentAddressRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentAddressId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentAddressId",
                table: "Users",
                column: "CurrentAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_CurrentAddressId",
                table: "Users",
                column: "CurrentAddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_CurrentAddressId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentAddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentAddressId",
                table: "Users");
        }
    }
}
