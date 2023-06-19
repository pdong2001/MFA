using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenCode", "CodeExpireTime", "FullName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("2dfd1e1e-7cfd-4580-92d3-f01fe17b7159"), "000000", new DateTime(2023, 6, 18, 18, 18, 19, 798, DateTimeKind.Utc).AddTicks(8725), "Test user two", "$2a$11$XoK9fp8FyIhNkaFL9nOwc.oAffLISUAUpfsaUR7dQ.eBZG9DQF7WC", "user2" },
                    { new Guid("826ab012-3c57-4abc-a399-807d5961ffe3"), "000000", new DateTime(2023, 6, 18, 18, 18, 19, 529, DateTimeKind.Utc).AddTicks(3874), "Test user one", "$2a$11$PzF3uBGA4gN8A3B0EjVLpObew8R6v6xObZZq797gUxDotzzZNn.wG", "user1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2dfd1e1e-7cfd-4580-92d3-f01fe17b7159"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("826ab012-3c57-4abc-a399-807d5961ffe3"));
        }
    }
}
