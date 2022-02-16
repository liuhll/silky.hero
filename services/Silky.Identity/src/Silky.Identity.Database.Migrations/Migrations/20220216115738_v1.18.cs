using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "706b9226-a4ae-45a1-adde-e4e9fa0e7372", "AQAAAAEAACcQAAAAEGFI3Dretz0XkW/MVyzxLoHNSqQhiY/BmpAvOCQb9a2ndduNI3otDIJFvyxDRq7Ihw==", "9365083f-13da-4ab3-8926-00fe3cd9a1a0" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41b5101a-c27c-4b0f-ad91-893b77c68c31", "AQAAAAEAACcQAAAAENJYnIhJidHprudo0T8Bg+SvDoZkHyfxHFhY/kaFUQlITvV055vgRgTZKRykRKcadQ==", "ca9e1ec7-c91b-4f85-8cfb-53d847e020ea" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
