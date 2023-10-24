using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Migrations
{
    /// <inheritdoc />
    public partial class Cleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Items",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Baths",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Beds",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guests",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl2",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl3",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rooms",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bookings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Baths",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Beds",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Guests",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ImageUrl2",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ImageUrl3",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderDate = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderItemPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }
    }
}
