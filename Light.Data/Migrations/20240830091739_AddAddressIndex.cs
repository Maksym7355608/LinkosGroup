using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Light.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressName",
                table: "Addresses",
                column: "AddressName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_AddressName",
                table: "Addresses");
        }
    }
}
