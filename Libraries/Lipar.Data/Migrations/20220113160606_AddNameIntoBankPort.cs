using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class AddNameIntoBankPort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                oldDefaultValue: new DateTime(2022, 1, 13, 19, 34, 17, 320, DateTimeKind.Local).AddTicks(5887));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Financial",
                table: "BankPorts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Financial",
                table: "BankPorts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 1, 13, 19, 34, 17, 320, DateTimeKind.Local).AddTicks(5887),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 1, 13, 19, 36, 5, 764, DateTimeKind.Local).AddTicks(7914));
        }
    }
}
