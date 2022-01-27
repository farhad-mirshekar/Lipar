using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementGeneralAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "Organization",
                table: "Centers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GenericAttributes",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyGroup = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Key = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueuedEmails",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FromName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    To = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ToName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAccountId = table.Column<int>(type: "int", nullable: false),
                    TimeSend = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuedEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueuedEmails_EmailAccounts_EmailAccountId",
                        column: x => x.EmailAccountId,
                        principalSchema: "General",
                        principalTable: "EmailAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueuedEmails_EmailAccountId",
                schema: "General",
                table: "QueuedEmails",
                column: "EmailAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenericAttributes",
                schema: "General");

            migrationBuilder.DropTable(
                name: "QueuedEmails",
                schema: "General");

            migrationBuilder.DropColumn(
                name: "Url",
                schema: "Organization",
                table: "Centers");
        }
    }
}
