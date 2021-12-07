using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "IsDefault", "IsDeleted", "IsPublic", "IsStatic", "Name", "NormalizedName", "RealName", "Sort", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, false, false, false, "admin", "ADMIN", "管理员", 0, null, null, null },
                    { 2L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, false, false, false, "normal", "NORMAL", "一般角色", 0, null, null, null }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1683bbf-6447-4933-abc0-a29967b7919e", "AQAAAAEAACcQAAAAEE4BXvcl8DARAjuUB4uk6/BZuDCOouGicWlSnguy1jNiyaiMsdDEJDiQAms+7wMaLA==", "9de01029-cc54-4d74-a24b-b7b6315f7dbc" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f93d39f9-a88f-42ab-836e-5f10fd307664", "AQAAAAEAACcQAAAAEJedqOtWTcdF+M7PITOp733ExbBEDga92fuzpckMaxiHT/sBgyk1Qp+LZ83B3x/82A==", "bf82807e-2d3f-4c0a-9d09-d3bda411c386" });
        }
    }
}
