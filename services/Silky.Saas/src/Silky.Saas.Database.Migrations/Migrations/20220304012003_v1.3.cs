using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Saas.Database.Migrations.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "Tenants",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DefaultValue",
                table: "Features",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Code", "Description", "FeatureCatalogId", "Name" },
                values: new object[] { "EnabledMenuManage", "允许授权用户登录微服务管理端.", 3L, "启用微服务管理端" });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedTime", "DefaultValue", "DeletedBy", "DeletedTime", "Description", "FeatureCatalogId", "FeatureType", "IsDeleted", "Name", "Options", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 5L, "EnabledSaasManage", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0, null, null, "在应用程序中启动Saas管理.", 4L, 1, false, "启用Saas管理", null, null, null });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RealName",
                value: "Silky");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 2L,
                column: "RealName",
                value: "Hero");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DropColumn(
                name: "RealName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "Features");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Code", "Description", "FeatureCatalogId", "Name" },
                values: new object[] { "EnabledSaasManage", "在应用程序中启动Saas管理.", 4L, "启用Saas管理" });
        }
    }
}
