using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Position.Database.Migrations.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Positions");

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "736ee5e8-18af-4e01-861e-4adadae13525");

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "1b24a1a4-b091-4f66-8e15-af74dfecd4d5");

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "d5d75344-4e00-4329-a580-14bee0c90b75");

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "3919966e-faff-46a6-864b-9358c0288e7d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Positions",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "zjl", "c264c987-090c-4e65-b8b8-33dd253d6c7c" });

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "jszj", "443d654b-3ca5-4e56-95a7-04fdfdc48609" });

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "htzz", "b3255f48-b7b5-40d0-81df-c6b9bf2b6870" });

            migrationBuilder.UpdateData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "qtzz", "cbcc1e29-56e5-467d-afb0-6111aca44a2a" });
        }
    }
}
