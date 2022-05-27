using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class addfieldinorderpaymentstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationNumber",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseMessage",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponseStatus",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RetrivalRefNo",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemTraceNo",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationNumber",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "ResponseMessage",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "ResponseStatus",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "RetrivalRefNo",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "SystemTraceNo",
                schema: "Application",
                table: "OrderPaymentStatuses");
        }
    }
}
