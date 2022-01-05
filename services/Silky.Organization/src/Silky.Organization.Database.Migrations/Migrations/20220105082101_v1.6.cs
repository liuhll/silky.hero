using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Organization.Database.Migrations.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Organizations",
                type: "varchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldMaxLength: 512)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "IsDeleted", "Name", "ParentId", "Remark", "Sort", "Status", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 1L, "silky.hero", "bbcfb581-3c2f-4156-8c2f-13a1f8a1f829", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Silky.Hero社区", null, null, 1, 1, null, null, null });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "IsDeleted", "Name", "ParentId", "Remark", "Sort", "Status", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 2L, "silky.hero.dev", "38cbab7e-a8f4-42b7-88f8-1dd830953e6d", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Silky.Hero开发部", 1L, "负责产品开发", 2, 1, null, null, null });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "IsDeleted", "Name", "ParentId", "Remark", "Sort", "Status", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 5L, "silky.hero.test", "ef88d22a-dfca-49d4-a7b0-0d85a373f07a", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Silky.Hero测试部", 1L, "负责产品测试", 4, 1, null, null, null });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "IsDeleted", "Name", "ParentId", "Remark", "Sort", "Status", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 6L, "silky.hero.product", "22c4acd9-63fb-4fb3-a48c-f8ada067a737", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Silky.Hero产品部", 1L, "负责产品规划、设计", 6, 1, null, null, null });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "IsDeleted", "Name", "ParentId", "Remark", "Sort", "Status", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 3L, "silky.hero.dev.services", "4bdaec7b-3a31-490e-af9b-d5b9d6873233", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Silky.Hero后端开发部", 2L, "负责服务段接口开发", 3, 1, null, null, null });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "IsDeleted", "Name", "ParentId", "Remark", "Sort", "Status", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 4L, "silky.hero.dev.front", "5bed6569-07b5-48e6-a996-4272fdf6d5f7", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, "Silky.Hero前端开发部", 2L, "负责前端UI、交互开发", 4, 1, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Remark",
                keyValue: null,
                column: "Remark",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Organizations",
                type: "varchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldMaxLength: 512,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
