using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedDate", "Name", "Salary", "UAN" },
                values: new object[,]
                {
                    { 20, new DateTime(2024, 11, 2, 3, 45, 15, 554, DateTimeKind.Local).AddTicks(390), "Ayesha", 45000.0, "qwertyuiop12345" },
                    { 21, new DateTime(2024, 11, 2, 3, 45, 15, 554, DateTimeKind.Local).AddTicks(405), "Ayesha", 45000.0, "poiuytrewq54321" },
                    { 22, new DateTime(2024, 11, 2, 3, 45, 15, 554, DateTimeKind.Local).AddTicks(407), "Ayesha", 45000.0, "mnbvcxz12345" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedDate", "Name", "Salary", "UAN" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 14, 1, 6, 59, 106, DateTimeKind.Local).AddTicks(4158), "Saif", 17000.0, "qwertyuiop12345" },
                    { 2, new DateTime(2024, 9, 14, 1, 6, 59, 106, DateTimeKind.Local).AddTicks(4175), "Ayesha", 5000.0, "poiuytrewq54321" },
                    { 3, new DateTime(2024, 9, 14, 1, 6, 59, 106, DateTimeKind.Local).AddTicks(4178), "Anas", 0.0, "mnbvcxz12345" }
                });
        }
    }
}
