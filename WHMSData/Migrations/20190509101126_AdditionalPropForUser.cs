using Microsoft.EntityFrameworkCore.Migrations;

namespace WHMSWebApp2.Data.Migrations
{
    public partial class AdditionalPropForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Partners",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_CreatorId",
                table: "Partners",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_AspNetUsers_CreatorId",
                table: "Partners",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_AspNetUsers_CreatorId",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_Partners_CreatorId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Partners");
        }
    }
}
