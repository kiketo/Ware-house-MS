using Microsoft.EntityFrameworkCore.Migrations;

namespace WHMSWebApp2.Data.Migrations
{
    public partial class OrderRevised : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductWarehouse",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_OrderId",
                table: "ProductWarehouse",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Orders_OrderId",
                table: "ProductWarehouse",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Orders_OrderId",
                table: "ProductWarehouse");

            migrationBuilder.DropIndex(
                name: "IX_ProductWarehouse_OrderId",
                table: "ProductWarehouse");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductWarehouse");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
