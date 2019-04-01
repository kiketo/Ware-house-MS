using Microsoft.EntityFrameworkCore.Migrations;

namespace WHMSData.Migrations
{
    public partial class marginToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MarginInPercent",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MarginInPercent",
                table: "Products",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
