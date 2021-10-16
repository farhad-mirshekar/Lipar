using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class AddShoppingCartItemIdIntoShoppingCartItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartItemId",
                schema: "Application",
                table: "ShoppingCartItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 24, 12, 41, 46, 620, DateTimeKind.Local).AddTicks(2247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 23, 20, 7, 32, 88, DateTimeKind.Local).AddTicks(5003));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoppingCartItemId",
                schema: "Application",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 23, 20, 7, 32, 88, DateTimeKind.Local).AddTicks(5003),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 24, 12, 41, 46, 620, DateTimeKind.Local).AddTicks(2247));
        }
    }
}
