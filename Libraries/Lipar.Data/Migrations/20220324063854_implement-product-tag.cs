using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementproducttag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductTags",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product_Tag_Mappings",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Tag_Mappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Tag_Mappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Application",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Tag_Mappings_ProductTags_ProductTagId",
                        column: x => x.ProductTagId,
                        principalSchema: "Application",
                        principalTable: "ProductTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Tag_Mappings_ProductId",
                schema: "Application",
                table: "Product_Tag_Mappings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Tag_Mappings_ProductTagId",
                schema: "Application",
                table: "Product_Tag_Mappings",
                column: "ProductTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_Tag_Mappings",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ProductTags",
                schema: "Application");
        }
    }
}
