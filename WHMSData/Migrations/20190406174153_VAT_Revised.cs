using Microsoft.EntityFrameworkCore.Migrations;

namespace WHMSData.Migrations
{
    public partial class VAT_Revised : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VAT",
                table: "Partners",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VAT",
                table: "Partners",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11,
                oldNullable: true);
        }
    }
}
