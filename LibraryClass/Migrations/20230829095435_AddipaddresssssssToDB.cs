using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryClass.Migrations
{
    /// <inheritdoc />
    public partial class AddipaddresssssssToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastPassword1",
                table: "registers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastPassword2",
                table: "registers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPassword1",
                table: "registers");

            migrationBuilder.DropColumn(
                name: "LastPassword2",
                table: "registers");
        }
    }
}
