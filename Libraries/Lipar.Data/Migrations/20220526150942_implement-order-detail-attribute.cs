using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class implementorderdetailattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPaymentStatuses_BankPorts_BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderPaymentStatuses_BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            //migrationBuilder.RenameColumn(
            //    name: "ShippingCartRate",
            //    schema: "Application",
            //    table: "Orders",
            //    newName: "ShoppingCartRate");

            migrationBuilder.AddColumn<Guid>(
                name: "BankPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserAddressId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShippingCostPriority",
                schema: "Application",
                table: "OrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryDatePriority",
                schema: "Application",
                table: "OrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "OrderDetailAttributes",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductAttributeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    IsPreSelected = table.Column<bool>(type: "bit", nullable: false),
                    ProductAttributeMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailAttributes_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalSchema: "Application",
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentStatuses_BankPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "BankPortId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailAttributes_OrderDetailId",
                schema: "Application",
                table: "OrderDetailAttributes",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPaymentStatuses_BankPorts_BankPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "BankPortId",
                principalSchema: "Financial",
                principalTable: "BankPorts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPaymentStatuses_BankPorts_BankPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropTable(
                name: "OrderDetailAttributes",
                schema: "Application");

            migrationBuilder.DropIndex(
                name: "IX_OrderPaymentStatuses_BankPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "BankPortId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            migrationBuilder.DropColumn(
                name: "UserAddressId",
                schema: "Application",
                table: "OrderPaymentStatuses");

            //migrationBuilder.RenameColumn(
            //    name: "ShoppingCartRate",
            //    schema: "Application",
            //    table: "Orders",
            //    newName: "ShippingCartRate");

            migrationBuilder.AddColumn<Guid>(
                name: "BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "ShippingCostPriority",
                schema: "Application",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryDatePriority",
                schema: "Application",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentStatuses_BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "BanlPortId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPaymentStatuses_BankPorts_BanlPortId",
                schema: "Application",
                table: "OrderPaymentStatuses",
                column: "BanlPortId",
                principalSchema: "Financial",
                principalTable: "BankPorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
