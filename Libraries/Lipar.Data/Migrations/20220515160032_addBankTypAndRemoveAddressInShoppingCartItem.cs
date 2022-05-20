using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class addBankTypAndRemoveAddressInShoppingCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Application",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "BankTypeId",
                schema: "Financial",
                table: "Banks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankTypeId",
                schema: "Financial",
                table: "Banks");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Application",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
