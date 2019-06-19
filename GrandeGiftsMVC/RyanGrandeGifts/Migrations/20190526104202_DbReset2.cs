using Microsoft.EntityFrameworkCore.Migrations;

namespace RyanGrandeGifts.Migrations
{
    public partial class DbReset2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamperProducts_TblHamper_HamperId",
                table: "TblHamperProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblHamperProducts_TblProduct_ProductId",
                table: "TblHamperProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblHamperProducts",
                table: "TblHamperProducts");

            migrationBuilder.DropIndex(
                name: "IX_TblHamperProducts_HamperId",
                table: "TblHamperProducts");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "TblHamperProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HamperId",
                table: "TblHamperProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HamperProductId",
                table: "TblHamperProducts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblHamperProducts",
                table: "TblHamperProducts",
                columns: new[] { "HamperId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamperProducts_TblHamper_HamperId",
                table: "TblHamperProducts",
                column: "HamperId",
                principalTable: "TblHamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamperProducts_TblProduct_ProductId",
                table: "TblHamperProducts",
                column: "ProductId",
                principalTable: "TblProduct",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamperProducts_TblHamper_HamperId",
                table: "TblHamperProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_TblHamperProducts_TblProduct_ProductId",
                table: "TblHamperProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblHamperProducts",
                table: "TblHamperProducts");

            migrationBuilder.AlterColumn<string>(
                name: "HamperProductId",
                table: "TblHamperProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "TblHamperProducts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "HamperId",
                table: "TblHamperProducts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblHamperProducts",
                table: "TblHamperProducts",
                column: "HamperProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperProducts_HamperId",
                table: "TblHamperProducts",
                column: "HamperId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamperProducts_TblHamper_HamperId",
                table: "TblHamperProducts",
                column: "HamperId",
                principalTable: "TblHamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamperProducts_TblProduct_ProductId",
                table: "TblHamperProducts",
                column: "ProductId",
                principalTable: "TblProduct",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
