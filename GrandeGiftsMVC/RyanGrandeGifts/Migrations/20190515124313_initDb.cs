using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RyanGrandeGifts.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblCategory",
                columns: table => new
                {
                    CategoryId = table.Column<string>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true),
                    CategoryDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TblAddress",
                columns: table => new
                {
                    AddressId = table.Column<string>(nullable: false),
                    UnitNumber = table.Column<int>(nullable: true),
                    StreetNumber = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    Suburb = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAddress", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_TblAddress_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblOrder",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrder", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_TblOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblHamper",
                columns: table => new
                {
                    HamperId = table.Column<string>(nullable: false),
                    HamperName = table.Column<string>(nullable: true),
                    HamperDescription = table.Column<string>(nullable: true),
                    HamperPrice = table.Column<double>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHamper", x => x.HamperId);
                    table.ForeignKey(
                        name: "FK_TblHamper_TblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TblCategory",
                        principalColumn: "CategoryId",
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
                name: "TblPicture",
                columns: table => new
                {
                    PictureId = table.Column<string>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileContent = table.Column<byte[]>(nullable: true),
                    ContentSize = table.Column<long>(nullable: false),
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
                name: "TblFeedback",
                columns: table => new
                {
                    FeedbackId = table.Column<string>(nullable: false),
                    FeedbackTitle = table.Column<string>(nullable: true),
                    FeedbackMessage = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    HamperOrderId = table.Column<string>(nullable: true)
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
                name: "IX_TblAddress_CustomerId",
                table: "TblAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TblFeedback_HamperOrderId",
                table: "TblFeedback",
                column: "HamperOrderId",
                unique: true,
                filter: "[HamperOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_CategoryId",
                table: "TblHamper",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperOrder_HamperId",
                table: "TblHamperOrder",
                column: "HamperId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperOrder_OrderId",
                table: "TblHamperOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrder_CustomerId",
                table: "TblOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TblPicture_HamperId",
                table: "TblPicture",
                column: "HamperId",
                unique: true,
                filter: "[HamperId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblAddress");

            migrationBuilder.DropTable(
                name: "TblFeedback");

            migrationBuilder.DropTable(
                name: "TblPicture");

            migrationBuilder.DropTable(
                name: "TblHamperOrder");

            migrationBuilder.DropTable(
                name: "TblHamper");

            migrationBuilder.DropTable(
                name: "TblOrder");

            migrationBuilder.DropTable(
                name: "TblCategory");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
