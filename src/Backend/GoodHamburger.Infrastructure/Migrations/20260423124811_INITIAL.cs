using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoodHamburger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class INITIAL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductImage_ProductImageId",
                        column: x => x.ProductImageId,
                        principalTable: "ProductImage",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Active", "CreatedAt", "Description", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("071857e9-1dfb-4b81-8300-9b025b4fbf0a"), true, new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Lanche", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("13cecac6-961b-474a-9843-553a057a6c11"), true, new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Acompanhamento", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3f6577f1-3483-4c64-8cbc-d68f5f0c26e1"), true, new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Bebida", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "Active", "ContentType", "CreatedAt", "FileName", "ImageBytes", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("22f4ce90-0763-4b1f-a96b-4df20d457de8"), true, "application/octet-stream", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "batata-frita-placeholder.txt", new byte[0], new Guid("e1477ac0-f99a-4b48-a220-3be6d573c2eb"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("32ccdd73-0f6d-4e27-949b-f95f08c5f03c"), true, "application/octet-stream", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "x-bacon-placeholder.txt", new byte[0], new Guid("2f8dd6da-2d2e-4f9b-bab0-bd2ebd56d53f"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8de4bfd9-92f9-4f0f-b4dc-94e2599559d1"), true, "application/octet-stream", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "x-burger-placeholder.txt", new byte[0], new Guid("d9dc5f2b-4f9d-4f7a-8e36-03dcee6eced6"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a1f83a8d-3ab8-4279-9e0f-38a7c4e67586"), true, "application/octet-stream", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "x-egg-placeholder.txt", new byte[0], new Guid("8f15f4af-8944-43f5-a94d-d3d31b2be7a6"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cfebf37a-b07f-4adc-8fc3-1f8bfd70f6f0"), true, "application/octet-stream", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "refrigerante-placeholder.txt", new byte[0], new Guid("e36881ad-5bf0-47c7-afba-2d4b7fdc5f77"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "CategoryId", "CreatedAt", "Description", "Name", "Price", "ProductImageId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2f8dd6da-2d2e-4f9b-bab0-bd2ebd56d53f"), true, new Guid("071857e9-1dfb-4b81-8300-9b025b4fbf0a"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "X Bacon", "X Bacon", 7.00m, new Guid("32ccdd73-0f6d-4e27-949b-f95f08c5f03c"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8f15f4af-8944-43f5-a94d-d3d31b2be7a6"), true, new Guid("071857e9-1dfb-4b81-8300-9b025b4fbf0a"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "X Egg", "X Egg", 4.50m, new Guid("a1f83a8d-3ab8-4279-9e0f-38a7c4e67586"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d9dc5f2b-4f9d-4f7a-8e36-03dcee6eced6"), true, new Guid("071857e9-1dfb-4b81-8300-9b025b4fbf0a"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "X Burger", "X Burger", 5.00m, new Guid("8de4bfd9-92f9-4f0f-b4dc-94e2599559d1"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e1477ac0-f99a-4b48-a220-3be6d573c2eb"), true, new Guid("13cecac6-961b-474a-9843-553a057a6c11"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Batata frita", "Batata frita", 2.00m, new Guid("22f4ce90-0763-4b1f-a96b-4df20d457de8"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e36881ad-5bf0-47c7-afba-2d4b7fdc5f77"), true, new Guid("3f6577f1-3483-4c64-8cbc-d68f5f0c26e1"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Refrigerante", "Refrigerante", 2.50m, new Guid("cfebf37a-b07f-4adc-8fc3-1f8bfd70f6f0"), new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductImageId",
                table: "Products",
                column: "ProductImageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ProductImage");
        }
    }
}
