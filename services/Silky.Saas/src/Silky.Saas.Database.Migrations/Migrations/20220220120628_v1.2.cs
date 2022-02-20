using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Saas.Database.Migrations.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EditionFeatures",
                keyColumn: "Id",
                keyValue: 8L,
                column: "EditionId",
                value: 2L);

            migrationBuilder.UpdateData(
                table: "EditionFeatures",
                keyColumn: "Id",
                keyValue: 10L,
                column: "EditionId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "EditionFeatures",
                keyColumn: "Id",
                keyValue: 11L,
                column: "EditionId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "EditionFeatures",
                keyColumn: "Id",
                keyValue: 12L,
                column: "EditionId",
                value: 3L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EditionFeatures",
                keyColumn: "Id",
                keyValue: 8L,
                column: "EditionId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "EditionFeatures",
                keyColumn: "Id",
                keyValue: 10L,
                column: "EditionId",
                value: 2L);

            migrationBuilder.UpdateData(
                table: "EditionFeatures",
                keyColumn: "Id",
                keyValue: 11L,
                column: "EditionId",
                value: 2L);

            migrationBuilder.UpdateData(
                table: "EditionFeatures",
                keyColumn: "Id",
                keyValue: 12L,
                column: "EditionId",
                value: 1L);
        }
    }
}
