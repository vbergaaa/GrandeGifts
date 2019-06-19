using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RyanGrandeGifts.Migrations
{
    public partial class DbReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblFeedback");

            migrationBuilder.DropTable(
                name: "TblPicture");

            migrationBuilder.DropTable(
                name: "TblHamperOrder");

            migrationBuilder.DropTable(
                name: "TblOrder");

            migrationBuilder.CreateTable(
                name: "TblProduct",
                columns: table => new
                {
                    ProductId = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProduct", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "TblHamperProducts",
                columns: table => new
                {
                    HamperProductId = table.Column<string>(nullable: false),
                    HamperId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHamperProducts", x => x.HamperProductId);
                    table.ForeignKey(
                        name: "FK_TblHamperProducts_TblHamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "TblHamper",
                        principalColumn: "HamperId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblHamperProducts_TblProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TblProduct",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperProducts_HamperId",
                table: "TblHamperProducts",
                column: "HamperId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperProducts_ProductId",
                table: "TblHamperProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblHamperProducts");

            migrationBuilder.DropTable(
                name: "TblProduct");

            migrationBuilder.CreateTable(
                name: "TblOrder",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrder", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_TblOrder_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblPicture",
                columns: table => new
                {
                    PictureId = table.Column<string>(nullable: false),
                    ContentSize = table.Column<long>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    FileContent = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    HamperId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPicture", x => x.PictureId);
                    table.ForeignKey(
                        name: "FK_TblPicture_TblHamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "TblHamper",
                        principalColumn: "HamperId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblHamperOrder",
                columns: table => new
                {
                    HamperOrderId = table.Column<string>(nullable: false),
                    HamperId = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHamperOrder", x => x.HamperOrderId);
                    table.ForeignKey(
                        name: "FK_TblHamperOrder_TblHamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "TblHamper",
                        principalColumn: "HamperId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblHamperOrder_TblOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "TblOrder",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblFeedback",
                columns: table => new
                {
                    FeedbackId = table.Column<string>(nullable: false),
                    FeedbackMessage = table.Column<string>(nullable: true),
                    FeedbackTitle = table.Column<string>(nullable: true),
                    HamperOrderId = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblFeedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_TblFeedback_TblHamperOrder_HamperOrderId",
                        column: x => x.HamperOrderId,
                        principalTable: "TblHamperOrder",
                        principalColumn: "HamperOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblFeedback_HamperOrderId",
                table: "TblFeedback",
                column: "HamperOrderId",
                unique: true,
                filter: "[HamperOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperOrder_HamperId",
                table: "TblHamperOrder",
                column: "HamperId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperOrder_OrderId",
                table: "TblHamperOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrder_ApplicationUserId",
                table: "TblOrder",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblPicture_HamperId",
                table: "TblPicture",
                column: "HamperId",
                unique: true,
                filter: "[HamperId] IS NOT NULL");
        }
    }
}
