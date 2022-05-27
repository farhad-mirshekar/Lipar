using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class removerelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPaymentStatuses_ShoppingCartItems_ShoppingCartItemId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderPaymentStatuses_ShoppingCartItemId",
                schema: "Application",
                table: "OrderPaymentStatuses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentStatuses_ShoppingCartItemId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "ShoppingCartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPaymentStatuses_ShoppingCartItems_ShoppingCartItemId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "ShoppingCartItemId",
                principalSchema: "Application",
                principalTable: "ShoppingCartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
