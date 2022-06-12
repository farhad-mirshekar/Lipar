using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementordertrackingdocstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                schema: "Application",
                table: "OrderTrackings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderTrackingDocStates",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 6, 12, 20, 32, 58, 375, DateTimeKind.Local).AddTicks(8415))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTrackingDocStates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrackingFlows_FromDocStateId",
                schema: "Application",
                table: "OrderTrackingFlows",
                column: "FromDocStateId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrackingFlows_ToDocStateId",
                schema: "Application",
                table: "OrderTrackingFlows",
                column: "ToDocStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTrackingFlows_OrderTrackingDocStates_FromDocStateId",
                schema: "Application",
                table: "OrderTrackingFlows",
                column: "FromDocStateId",
                principalSchema: "Application",
                principalTable: "OrderTrackingDocStates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTrackingFlows_OrderTrackingDocStates_ToDocStateId",
                schema: "Application",
                table: "OrderTrackingFlows",
                column: "ToDocStateId",
                principalSchema: "Application",
                principalTable: "OrderTrackingDocStates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTrackingFlows_OrderTrackingDocStates_FromDocStateId",
                schema: "Application",
                table: "OrderTrackingFlows");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTrackingFlows_OrderTrackingDocStates_ToDocStateId",
                schema: "Application",
                table: "OrderTrackingFlows");

            migrationBuilder.DropTable(
                name: "OrderTrackingDocStates",
                schema: "Application");

            migrationBuilder.DropIndex(
                name: "IX_OrderTrackingFlows_FromDocStateId",
                schema: "Application",
                table: "OrderTrackingFlows");

            migrationBuilder.DropIndex(
                name: "IX_OrderTrackingFlows_ToDocStateId",
                schema: "Application",
                table: "OrderTrackingFlows");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                schema: "Application",
                table: "OrderTrackings");
        }
    }
}
