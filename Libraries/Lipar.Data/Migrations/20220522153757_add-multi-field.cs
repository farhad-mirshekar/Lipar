using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class addmultifield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "VerifyUri",
                schema: "Financial",
                table: "Banks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentStatuses_BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "BanlPortId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentStatuses_UserId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPaymentStatuses_BankPorts_BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "BanlPortId",
                principalSchema: "Financial",
                principalTable: "BankPorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPaymentStatuses_Users_UserId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "UserId",
                principalSchema: "Organization",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPaymentStatuses_BankPorts_BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPaymentStatuses_Users_UserId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderPaymentStatuses_BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderPaymentStatuses_UserId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "VerifyUri",
                schema: "Financial",
                table: "Banks");
        }
    }
}
