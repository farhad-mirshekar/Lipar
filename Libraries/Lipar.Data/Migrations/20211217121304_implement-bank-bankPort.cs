using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementbankbankPort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Financial");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 12, 17, 15, 43, 3, 430, DateTimeKind.Local).AddTicks(9514),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 24, 12, 41, 46, 620, DateTimeKind.Local).AddTicks(2247));

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "Financial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PaymentUri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ServiceUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectUri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TransactionCost = table.Column<int>(type: "int", nullable: true),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Banks_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankPorts",
                schema: "Financial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    MerchantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Password = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    TerminalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankPorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankPorts_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "Financial",
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankPorts_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankPorts_BankId",
                schema: "Financial",
                table: "BankPorts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankPorts_EnabledTypeId",
                schema: "Financial",
                table: "BankPorts",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_EnabledTypeId",
                schema: "Financial",
                table: "Banks",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UserId",
                schema: "Financial",
                table: "Banks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankPorts",
                schema: "Financial");

            migrationBuilder.DropTable(
                name: "Banks",
                schema: "Financial");

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
                oldDefaultValue: new DateTime(2021, 12, 17, 15, 43, 3, 430, DateTimeKind.Local).AddTicks(9514));
        }
    }
}
