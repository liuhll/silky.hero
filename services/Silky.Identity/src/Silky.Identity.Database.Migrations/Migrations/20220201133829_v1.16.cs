using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v116 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Roles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78c159e9-227e-4530-b798-788034bdfcf3", "AQAAAAEAACcQAAAAEO0Cc/VPYNvR9sXTz0kKhxj2FQRrO/1NnebIdoeTvjouHVfb1YVo222cLMYaqoN9mg==", "e4d2051e-fa39-471a-955d-06b00e688686" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "668da29d-a9cd-434f-91da-44d4e0f228a2", "AQAAAAEAACcQAAAAEBkci36+c2ID6uFI4Iw1RLz3ivSoT2G3oyt342j2dMAAbQR/1lyMONhU+25K28IliA==", "b40e9bae-b1ed-4cfd-97c7-93ecda7dcf32" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c643a30d-9ce8-49b2-9b46-6924096b733a", "AQAAAAEAACcQAAAAENU1dhCIRL7U4cC77XiNRyCERemYMp6jmZ4xwKBcAl9eAD5ef6YdPhRl6L4a1jNwaA==", "1717728e-bc42-45af-b7c2-f465fcad85a4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34390721-6b9e-493a-8bbb-1086ff8a6dbb", "AQAAAAEAACcQAAAAEIZ4gfXUlXUh5aFsb0ExeexJO6RntRZd+v088re10VyTdz7bXdasZScn95kZWhnjBg==", "a815bf62-16d3-4fb8-bbea-6df04ba56812" });
        }
    }
}
