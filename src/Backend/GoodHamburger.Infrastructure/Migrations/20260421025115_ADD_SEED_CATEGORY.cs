using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoodHamburger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADD_SEED_CATEGORY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Active", "CreatedAt", "Description", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("071857e9-1dfb-4b81-8300-9b025b4fbf0a"), true, new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Hamburgers", new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("13cecac6-961b-474a-9843-553a057a6c11"), true, new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Accompaniment", new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("071857e9-1dfb-4b81-8300-9b025b4fbf0a"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("13cecac6-961b-474a-9843-553a057a6c11"));
        }
    }
}
