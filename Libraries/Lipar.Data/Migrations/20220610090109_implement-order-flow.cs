using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementorderflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderTrackings",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTrackings_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Application",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTrackingFlows",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderTrackingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromPositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromDocStateId = table.Column<int>(type: "int", nullable: false),
                    ToPositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToDocStateId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTrackingFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTrackingFlows_OrderTrackings_OrderTrackingId",
                        column: x => x.OrderTrackingId,
                        principalSchema: "Application",
                        principalTable: "OrderTrackings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTrackingFlows_Positions_FromPositionId",
                        column: x => x.FromPositionId,
                        principalSchema: "Organization",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderTrackingFlows_Positions_ToPositionId",
                        column: x => x.ToPositionId,
                        principalSchema: "Organization",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrackingFlows_FromPositionId",
                schema: "Application",
                table: "OrderTrackingFlows",
                column: "FromPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrackingFlows_OrderTrackingId",
                schema: "Application",
                table: "OrderTrackingFlows",
                column: "OrderTrackingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrackingFlows_ToPositionId",
                schema: "Application",
                table: "OrderTrackingFlows",
                column: "ToPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrackings_OrderId",
                schema: "Application",
                table: "OrderTrackings",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTrackingFlows",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "OrderTrackings",
                schema: "Application");
        }
    }
}
