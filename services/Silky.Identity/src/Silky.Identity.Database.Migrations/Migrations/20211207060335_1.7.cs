using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDay", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "IsActive", "IsDeleted", "JobNumber", "LockoutEnd", "MobilePhone", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "RealName", "SecurityStamp", "Sex", "Surname", "TelPhone", "TenantId", "UpdatedBy", "UpdatedTime", "UserName" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f1683bbf-6447-4933-abc0-a29967b7919e", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@silky.com", true, false, null, null, "13111111111", "ADMIN@SILKY.COM", "ADMIN", "AQAAAAEAACcQAAAAEE4BXvcl8DARAjuUB4uk6/BZuDCOouGicWlSnguy1jNiyaiMsdDEJDiQAms+7wMaLA==", null, "9de01029-cc54-4d74-a24b-b7b6315f7dbc", null, null, null, null, null, null, "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDay", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "IsActive", "IsDeleted", "JobNumber", "LockoutEnd", "MobilePhone", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "RealName", "SecurityStamp", "Sex", "Surname", "TelPhone", "TenantId", "UpdatedBy", "UpdatedTime", "UserName" },
                values: new object[] { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f93d39f9-a88f-42ab-836e-5f10fd307664", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "liuhll@silky.com", true, false, null, null, "13111111112", "LIUHLL@SILKY.COM", "LIUHLL", "AQAAAAEAACcQAAAAEJedqOtWTcdF+M7PITOp733ExbBEDga92fuzpckMaxiHT/sBgyk1Qp+LZ83B3x/82A==", null, "bf82807e-2d3f-4c0a-9d09-d3bda411c386", null, null, null, null, null, null, "liuhll" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
