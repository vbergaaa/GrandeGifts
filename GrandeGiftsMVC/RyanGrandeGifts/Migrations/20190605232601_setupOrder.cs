using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RyanGrandeGifts.Migrations
{
    public partial class setupOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblOrders",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_TblOrders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblHamperOrders",
                columns: table => new
                {
                    HamperOrderId = table.Column<string>(nullable: false),
                    HamperId = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHamperOrders", x => x.HamperOrderId);
                    table.ForeignKey(
                        name: "FK_TblHamperOrders_TblHamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "TblHamper",
                        principalColumn: "HamperId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblHamperOrders_TblOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "TblOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperOrders_HamperId",
                table: "TblHamperOrders",
                column: "HamperId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperOrders_OrderId",
                table: "TblHamperOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrders_ApplicationUserId",
                table: "TblOrders",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblHamperOrders");

            migrationBuilder.DropTable(
                name: "TblOrders");
        }
    }
}
