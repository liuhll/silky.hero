using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Organization.Database.Migrations.Migrations
{
    public partial class v17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "42dab193-fb45-42f6-9e7c-eedd2c9dd9e7", 1L });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "e9ce91b7-770c-459b-8751-f8986b0dc3bf", 1L });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "1376737b-0d6d-462d-a28b-c314c76332e2", 1L });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "f0695790-7154-45a8-b402-ebe84d1ecad1", 1L });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "e723f5b6-3155-47c8-b3b7-488b7697854a", 1L });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "ab444603-6d67-4af5-b1ed-c532a991a237", 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "bbcfb581-3c2f-4156-8c2f-13a1f8a1f829", null });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "38cbab7e-a8f4-42b7-88f8-1dd830953e6d", null });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "4bdaec7b-3a31-490e-af9b-d5b9d6873233", null });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "5bed6569-07b5-48e6-a996-4272fdf6d5f7", null });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "ef88d22a-dfca-49d4-a7b0-0d85a373f07a", null });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "ConcurrencyStamp", "TenantId" },
                values: new object[] { "22c4acd9-63fb-4fb3-a48c-f8ada067a737", null });
        }
    }
}
