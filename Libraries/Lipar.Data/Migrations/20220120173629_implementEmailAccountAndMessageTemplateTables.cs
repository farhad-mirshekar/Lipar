using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementEmailAccountAndMessageTemplateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 1, 20, 21, 6, 28, 613, DateTimeKind.Local).AddTicks(5703),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 1, 20, 14, 34, 51, 466, DateTimeKind.Local).AddTicks(91));

            migrationBuilder.CreateTable(
                name: "EmailAccounts",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Host = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplates",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmailAccountId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageTemplates_EmailAccounts_EmailAccountId",
                        column: x => x.EmailAccountId,
                        principalSchema: "General",
                        principalTable: "EmailAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageTemplates_EmailAccountId",
                schema: "General",
                table: "MessageTemplates",
                column: "EmailAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageTemplates",
                schema: "General");

            migrationBuilder.DropTable(
                name: "EmailAccounts",
                schema: "General");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "Application",
                table: "ProductComments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 1, 20, 14, 34, 51, 466, DateTimeKind.Local).AddTicks(91),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 1, 20, 21, 6, 28, 613, DateTimeKind.Local).AddTicks(5703));
        }
    }
}
