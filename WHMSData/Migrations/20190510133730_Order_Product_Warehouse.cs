using Microsoft.EntityFrameworkCore.Migrations;

namespace WHMSWebApp2.Data.Migrations
{
    public partial class Order_Product_Warehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "OrderProductWarehouses",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    WantedQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductWarehouses", x => new { x.OrderId, x.ProductId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_OrderProductWarehouses_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductWarehouses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductWarehouses_ProductId",
                table: "OrderProductWarehouses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductWarehouses_WarehouseId",
                table: "OrderProductWarehouses",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProductWarehouses");

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
    }
}
