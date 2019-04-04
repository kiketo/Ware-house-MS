using Microsoft.EntityFrameworkCore.Migrations;

namespace WHMSData.Migrations
{
    public partial class Edited_PartnerName_ToBE_Unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Partners",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_Name",
                table: "Partners",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Partners_Name",
                table: "Partners");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Partners",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
