using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WHMSData.Migrations
{
    public partial class ProductOrderWarehouse_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_WarehouseId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Products_ProductId",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Transfers_TransferId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TransferId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Partners_ProductId",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TransferId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AddressText",
                table: "Addresses",
                newName: "Text");

            migrationBuilder.AddColumn<decimal>(
                name: "SellPrice",
                table: "Products",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductOrderWarehouse",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderWarehouse", x => new { x.ProductId, x.OrderId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_ProductOrderWarehouse_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderWarehouse_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderWarehouse_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderWarehouse_OrderId",
                table: "ProductOrderWarehouse",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderWarehouse_WarehouseId",
                table: "ProductOrderWarehouse",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrderWarehouse");

            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Addresses",
                newName: "AddressText");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransferId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Partners",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    FromWarehouseId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ToWarehouseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Warehouses_FromWarehouseId",
                        column: x => x.FromWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Warehouses_ToWarehouseId",
                        column: x => x.ToWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TransferId",
                table: "Products",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_ProductId",
                table: "Partners",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromWarehouseId",
                table: "Transfers",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToWarehouseId",
                table: "Transfers",
                column: "ToWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_WarehouseId",
                table: "Orders",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Products_ProductId",
                table: "Partners",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Transfers_TransferId",
                table: "Products",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
