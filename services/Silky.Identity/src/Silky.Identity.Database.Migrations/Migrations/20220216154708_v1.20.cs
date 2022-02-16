using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v120 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDay",
                table: "Users",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "BirthDay", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "68972256-4df6-4d05-a55c-525eab7c01e2", "AQAAAAEAACcQAAAAEGjXPT8+TYrfXtYplLwNx2G3s1Yi1REJCWuPmQj6LXnNW4rfqxaOiyH/Uq2opongcw==", "280de1cb-4724-4438-b90a-f5d7265febb3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "BirthDay", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "9210ded5-77ef-4739-8ae2-ce34ce0be4f9", "AQAAAAEAACcQAAAAEKBMQcctobzCKh+OYa0PfHqc4tq6p0gfQNXTQRadspuh/pEJL5R4EqyqYxzp9mIZDg==", "04e7fb1d-731c-4e4b-9b1c-2eb1cd8e7043" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDay",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "BirthDay", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "546dac6d-2a45-4b19-a0ba-89b24669b659", "AQAAAAEAACcQAAAAEC0yjZdLsyj+otxcZkpPdxGgyEoUiiY0vtEBlAb34k6mvAN4CH9Gp/BlL4Hy0akTdA==", "2f708ed8-f814-47ba-8d44-561258fdd714" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "BirthDay", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "44408ed2-b740-4b58-ae45-1638e36d8d45", "AQAAAAEAACcQAAAAEMjxRMXUoQJKhGygmyXc/OXmXsIGfViKDRv4MkE1e7JXiMV38Z/Z4A0B2C+/XLzO7Q==", "99c6f88f-a604-41db-bb8e-0eb144830fc2" });
        }
    }
}
