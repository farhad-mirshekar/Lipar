using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementorderpaymentstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderPaymentStatuses",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShoppingCartItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderPaymentStatusTypeId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPaymentStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPaymentStatuses_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Application",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPaymentStatuses_ShoppingCartItems_ShoppingCartItemId",
                        column: x => x.ShoppingCartItemId,
                        principalSchema: "Application",
                        principalTable: "ShoppingCartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentStatuses_OrderId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentStatuses_ShoppingCartItemId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "ShoppingCartItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPaymentStatuses",
                schema: "Application");
        }
    }
}
