using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v117 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df3d16a2-ac45-4a9d-b31b-2e64fa24b2a5", "AQAAAAEAACcQAAAAEAXSfUXlYLSgVTyFzVb1MIGbE0c0mjJujd0ndu+i7cmobgQx25YuDfqLiqdeWguZHg==", "37bacc0b-b09e-42f2-a3e7-10c50f80961d" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "556368d2-1dab-44f8-ba68-6712261c874f", "AQAAAAEAACcQAAAAEC+bfkBKtM7vqKOqa6NqnNOjr2R3J54EDIJFF95MKyFPu3NXcOKmV+kpfvSGjS/y7Q==", "ca3f1adc-b3a2-4b0f-a41f-9fc919a51e1c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Roles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

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
    }
}
