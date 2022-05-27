using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class addtokensigndata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SignData",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignData",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "Token",
                schema: "Application",
                table: "OrderPaymentStatuses");
        }
    }
}
