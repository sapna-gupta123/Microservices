using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CategoryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingCategoryType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1d4a151b-0171-4e26-818a-1fdbcf3f3ba7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b2532bd0-997b-4a65-b507-a1e32c6dc598"));

            migrationBuilder.AddColumn<string>(
                name: "CategoryType",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryType", "Name" },
                values: new object[,]
                {
                    { new Guid("45b72a03-15ec-4729-9701-92a0a3b5e036"), "Electronics", "Mobile" },
                    { new Guid("f7e9da1b-8cbc-49db-be26-459a3029ee14"), "HomeAppliances", "TV" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("45b72a03-15ec-4729-9701-92a0a3b5e036"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f7e9da1b-8cbc-49db-be26-459a3029ee14"));

            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1d4a151b-0171-4e26-818a-1fdbcf3f3ba7"), "Home Appliances" },
                    { new Guid("b2532bd0-997b-4a65-b507-a1e32c6dc598"), "Electronics" }
                });
        }
    }
}
