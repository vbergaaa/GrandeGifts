using Microsoft.EntityFrameworkCore.Migrations;

namespace RyanGrandeGifts.Migrations
{
    public partial class changecustomertoApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrder_AspNetUsers_CustomerId",
                table: "TblOrder");

            migrationBuilder.DropIndex(
                name: "IX_TblOrder_CustomerId",
                table: "TblOrder");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "TblOrder",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TblOrder",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblOrder_ApplicationUserId",
                table: "TblOrder",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrder_AspNetUsers_ApplicationUserId",
                table: "TblOrder",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrder_AspNetUsers_ApplicationUserId",
                table: "TblOrder");

            migrationBuilder.DropIndex(
                name: "IX_TblOrder_ApplicationUserId",
                table: "TblOrder");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TblOrder");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "TblOrder",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblOrder_CustomerId",
                table: "TblOrder",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrder_AspNetUsers_CustomerId",
                table: "TblOrder",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
