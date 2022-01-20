using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementOrderAndOrderDetailTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 1, 20, 14, 34, 51, 466, DateTimeKind.Local).AddTicks(91),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 1, 13, 19, 36, 5, 764, DateTimeKind.Local).AddTicks(7914));

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserAddressId = table.Column<int>(type: "int", nullable: false),
                    BankPortId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_BankPorts_BankPortId",
                        column: x => x.BankPortId,
                        principalSchema: "Financial",
                        principalTable: "BankPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributeProductJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShoppingCartItemJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Application",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "Application",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BankPortId",
                schema: "Application",
                table: "Orders",
                column: "BankPortId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Application");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 1, 13, 19, 36, 5, 764, DateTimeKind.Local).AddTicks(7914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 1, 20, 14, 34, 51, 466, DateTimeKind.Local).AddTicks(91));
        }
    }
}
