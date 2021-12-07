using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class _19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClaimTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "IsDeleted", "IsStatic", "Name", "Regex", "RegexDescription", "Required", "TenantId", "UpdatedBy", "UpdatedTime", "ValueType" },
                values: new object[,]
                {
                    { 1L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "name", null, null, false, null, null, null, 0 },
                    { 2L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "email", null, null, false, null, null, null, 0 },
                    { 3L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "sex", null, null, false, null, null, null, 0 },
                    { 4L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "mobilePhone", null, null, false, null, null, null, 0 },
                    { 5L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "telPhone", null, null, false, null, null, null, 0 },
                    { 6L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "surname", null, null, false, null, null, null, 0 },
                    { 7L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "realName", null, null, false, null, null, null, 0 },
                    { 8L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "jobNumber", null, null, false, null, null, null, 0 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f9be60a-b660-4c45-b543-5263b049919c", "AQAAAAEAACcQAAAAEHnMndKow19uorgZ26RzEayk0PM8TbRfOX16Wmd847AHEOmVEeH51GRiQEVVEUIoPg==", "7b64fbde-0c19-4a04-bf55-d374d7d1c977" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acb6f8a9-63b6-41e7-9c68-2caff38aba52", "AQAAAAEAACcQAAAAEIFEWtMvDD6tfRsfcyR7wG7P6X+/N19D95R52laincUazfRs6OtJau1ZDxZnjouSGw==", "2613f59a-a4fd-41ed-8664-1fd4373a9e96" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "ClaimTypes",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "504247f2-6f85-4276-8ea5-e46d9f1d4c4a", "AQAAAAEAACcQAAAAEAxj1PCDkpwToSrLX/XYptIvnv84Y49fOPoWE90JpTBYaZ1ZzFgmUeQtnMxp0Vo95Q==", "0da1cdfc-0fb5-43d8-8fc1-c6db5a3de207" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a2b58e3-ce1c-4837-b6ac-15055760e8e3", "AQAAAAEAACcQAAAAEJcjGcqqrmQee48tLsdhxOE9xUMQp4iO5U7PjRZ4GlZgIQv7N+Ny6lS3Kj4PZFWOjg==", "3ebf936f-e726-4c1e-bae0-e5ce9c994bcc" });
        }
    }
}
