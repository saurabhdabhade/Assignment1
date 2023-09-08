using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryClass.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialMigrationToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Cust_Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cust_FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cust_LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cust_Phone = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Cust_Email);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    ItemName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IRate = table.Column<int>(type: "int", nullable: false),
                    IQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.ItemName);
                });

            migrationBuilder.CreateTable(
                name: "placeOrders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cust_Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IQuantity = table.Column<int>(type: "int", nullable: false),
                    IRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_placeOrders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "registers",
                columns: table => new
                {
                    RegisterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Confirm_Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registers", x => x.RegisterID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "placeOrders");

            migrationBuilder.DropTable(
                name: "registers");
        }
    }
}
