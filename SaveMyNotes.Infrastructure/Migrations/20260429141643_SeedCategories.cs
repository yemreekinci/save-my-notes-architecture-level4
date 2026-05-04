using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SaveMyNotes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tags",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "Status", "UpdatedAt", "Color" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 4, 29, 14, 16, 42, 668, DateTimeKind.Utc).AddTicks(9032), "Genel", 0, null, "#808080" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 4, 29, 14, 16, 42, 668, DateTimeKind.Utc).AddTicks(9636), "İş", 0, null, "#0078D4" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 4, 29, 14, 16, 42, 668, DateTimeKind.Utc).AddTicks(9637), "Kişisel", 0, null, "#28A745" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categories");
        }
    }
}
