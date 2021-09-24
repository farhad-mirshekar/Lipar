using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class ChangeTableNameDiscountTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_DiscounTypes_DiscountTypeId",
                schema: "Application",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscounTypes",
                schema: "Application",
                table: "DiscounTypes");

            migrationBuilder.RenameTable(
                name: "DiscounTypes",
                schema: "Application",
                newName: "DiscountTypes",
                newSchema: "Application");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 11, 18, 2, 10, 595, DateTimeKind.Local).AddTicks(248),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 3, 17, 20, 56, 188, DateTimeKind.Local).AddTicks(2217));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountTypes",
                schema: "Application",
                table: "DiscountTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_DiscountTypes_DiscountTypeId",
                schema: "Application",
                table: "Products",
                column: "DiscountTypeId",
                principalSchema: "Application",
                principalTable: "DiscountTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_DiscountTypes_DiscountTypeId",
                schema: "Application",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountTypes",
                schema: "Application",
                table: "DiscountTypes");

            migrationBuilder.RenameTable(
                name: "DiscountTypes",
                schema: "Application",
                newName: "DiscounTypes",
                newSchema: "Application");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 3, 17, 20, 56, 188, DateTimeKind.Local).AddTicks(2217),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 11, 18, 2, 10, 595, DateTimeKind.Local).AddTicks(248));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscounTypes",
                schema: "Application",
                table: "DiscounTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_DiscounTypes_DiscountTypeId",
                schema: "Application",
                table: "Products",
                column: "DiscountTypeId",
                principalSchema: "Application",
                principalTable: "DiscounTypes",
                principalColumn: "Id");
        }
    }
}
