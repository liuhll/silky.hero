using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v115 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "TenantId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataRange", "TenantId" },
                values: new object[] { 1, 1L });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DataRange", "TenantId" },
                values: new object[] { 1, 1L });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "c643a30d-9ce8-49b2-9b46-6924096b733a", "AQAAAAEAACcQAAAAENU1dhCIRL7U4cC77XiNRyCERemYMp6jmZ4xwKBcAl9eAD5ef6YdPhRl6L4a1jNwaA==", "1717728e-bc42-45af-b7c2-f465fcad85a4", 1L });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "34390721-6b9e-493a-8bbb-1086ff8a6dbb", "AQAAAAEAACcQAAAAEIZ4gfXUlXUh5aFsb0ExeexJO6RntRZd+v088re10VyTdz7bXdasZScn95kZWhnjBg==", "a815bf62-16d3-4fb8-bbea-6df04ba56812", 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 8L,
                column: "TenantId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataRange", "TenantId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DataRange", "TenantId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "63d7423a-e677-443d-bb25-714e2fa4fd85", "AQAAAAEAACcQAAAAEIV36tN9sEaG5nylxMl6jYsefV0GgjY6wJVFS87TZyRtyZ30fW1zgt/iOFlws3MTMQ==", "7516cd69-b9a4-4cea-a215-66d8645af40c", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TenantId" },
                values: new object[] { "4c57ccda-7c03-44ec-8e01-46a70e43d17d", "AQAAAAEAACcQAAAAEH9HQqMZoi3seqPHjJ6iCQVz3y7PzjuWWoMX4zwDMrQrl3IjotB9mbDY2Gy+F+I9ew==", "627798fa-877e-4ec7-9bc5-4691229fd65a", null });
        }
    }
}
