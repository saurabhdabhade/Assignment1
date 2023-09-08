using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryClass.Migrations
{
    /// <inheritdoc />
    public partial class AddipaddressmigrateToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "placeOrders",
                newName: "ipAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ipAddress",
                table: "placeOrders",
                newName: "Address");
        }
    }
}
