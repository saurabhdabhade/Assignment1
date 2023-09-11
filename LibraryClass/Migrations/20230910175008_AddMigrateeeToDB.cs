using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryClass.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrateeeToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "registers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "placeOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "registers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "placeOrders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "customers");
        }
    }
}
