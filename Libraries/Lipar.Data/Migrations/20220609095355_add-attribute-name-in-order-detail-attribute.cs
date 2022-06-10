using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class addattributenameinorderdetailattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttributeName",
                schema: "Application",
                table: "OrderDetailAttributes",
                type: "NVARCHAR(1000)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeName",
                schema: "Application",
                table: "OrderDetailAttributes");
        }
    }
}
