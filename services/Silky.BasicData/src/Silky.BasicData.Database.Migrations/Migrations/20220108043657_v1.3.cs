using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.BasicData.Database.Migrations.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DictionaryItems",
                keyColumn: "Id",
                keyValue: 1L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "DictionaryItems",
                keyColumn: "Id",
                keyValue: 2L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "DictionaryTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "TenantId",
                value: 1L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DictionaryItems",
                keyColumn: "Id",
                keyValue: 1L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DictionaryItems",
                keyColumn: "Id",
                keyValue: 2L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "DictionaryTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "TenantId",
                value: null);
        }
    }
}
