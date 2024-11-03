using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    UAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
