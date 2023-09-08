using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryClass.Migrations
{
    /// <inheritdoc />
    public partial class AddFewColumnsUpdateToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EventDateTime",
                table: "registers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDateTime",
                table: "placeOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDateTime",
                table: "items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDateTime",
                table: "customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDateTime",
                table: "registers");

            migrationBuilder.DropColumn(
                name: "EventDateTime",
                table: "placeOrders");

            migrationBuilder.DropColumn(
                name: "EventDateTime",
                table: "items");

            migrationBuilder.DropColumn(
                name: "EventDateTime",
                table: "customers");
        }
    }
}
