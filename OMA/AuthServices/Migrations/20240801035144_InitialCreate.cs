using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthServices.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedDate", "Email", "IsActive", "LastLogin", "Name", "Password", "Roles" },
                values: new object[] { new Guid("2dde00f9-9fde-4bcf-800a-97976815dbe1"), new DateTime(2024, 8, 1, 9, 21, 43, 888, DateTimeKind.Local).AddTicks(1253), "admin@admin.com", true, new DateTime(2024, 8, 1, 9, 21, 43, 888, DateTimeKind.Local).AddTicks(1266), "Admin", "$OMAHASH$V1$10000$QUWEtS0guwFSX5TP/l77jHs1RUNKc7oxedCgSjPO626Um5na", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
