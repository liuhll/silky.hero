using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v119 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Status" },
                values: new object[] { "546dac6d-2a45-4b19-a0ba-89b24669b659", "AQAAAAEAACcQAAAAEC0yjZdLsyj+otxcZkpPdxGgyEoUiiY0vtEBlAb34k6mvAN4CH9Gp/BlL4Hy0akTdA==", "2f708ed8-f814-47ba-8d44-561258fdd714", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Status" },
                values: new object[] { "44408ed2-b740-4b58-ae45-1638e36d8d45", "AQAAAAEAACcQAAAAEMjxRMXUoQJKhGygmyXc/OXmXsIGfViKDRv4MkE1e7JXiMV38Z/Z4A0B2C+/XLzO7Q==", "99c6f88f-a604-41db-bb8e-0eb144830fc2", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "706b9226-a4ae-45a1-adde-e4e9fa0e7372", true, "AQAAAAEAACcQAAAAEGFI3Dretz0XkW/MVyzxLoHNSqQhiY/BmpAvOCQb9a2ndduNI3otDIJFvyxDRq7Ihw==", "9365083f-13da-4ab3-8926-00fe3cd9a1a0" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41b5101a-c27c-4b0f-ad91-893b77c68c31", true, "AQAAAAEAACcQAAAAENJYnIhJidHprudo0T8Bg+SvDoZkHyfxHFhY/kaFUQlITvV055vgRgTZKRykRKcadQ==", "ca9e1ec7-c91b-4f85-8cfb-53d847e020ea" });
        }
    }
}
