using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Permission.Database.Migrations.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Cache", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 1L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "组织架构", null, null, null, 999, 1, 0, null, null });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Cache", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 1000L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "权限管理", null, null, null, 998, 1, 0, null, null });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Cache", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 2L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "用户管理", 1L, "Identity.User", null, 999, 1, 1, null, null },
                    { 100L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "机构管理", 1L, "Organization.Default", null, 998, 1, 1, null, null },
                    { 200L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "职位管理", 1L, "Position.Default", null, 997, 1, 1, null, null },
                    { 1001L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "菜单管理", 1000L, "Permission.Menu", null, 999, 1, 1, null, null },
                    { 1100L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "角色管理", 1000L, "Identity.Role", null, 998, 1, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Cache", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 3L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "新增", 2L, "Identity.User.Create", null, 0, 1, 2, null, null },
                    { 4L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "编辑", 2L, "Identity.User.Update", null, 0, 1, 2, null, null },
                    { 5L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "删除", 2L, "Identity.User.Delete", null, 0, 1, 2, null, null },
                    { 6L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "锁定", 2L, "Identity.User.Lock", null, 0, 1, 2, null, null },
                    { 7L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "解锁", 2L, "Identity.User.UnLock", null, 0, 1, 2, null, null },
                    { 8L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "更新声明", 2L, "Identity.User.UpdateClaimTypes", null, 0, 1, 2, null, null },
                    { 101L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "新增", 100L, "Organization.Create", null, 0, 1, 2, null, null },
                    { 102L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "编辑", 100L, "Organization.Update", null, 0, 1, 2, null, null },
                    { 103L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "删除", 100L, "Organization.Delete", null, 0, 1, 2, null, null },
                    { 201L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "新增", 200L, "Position.Create", null, 0, 1, 2, null, null },
                    { 202L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "编辑", 200L, "Position.Update", null, 0, 1, 2, null, null },
                    { 203L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "删除", 200L, "Position.Delete", null, 0, 1, 2, null, null },
                    { 1002L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "新增", 1001L, "Permission.Menu.Create", null, 0, 1, 2, null, null },
                    { 1003L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "编辑", 1001L, "Permission.Menu.Update", null, 0, 1, 2, null, null },
                    { 1004L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "删除", 1001L, "Permission.MenuDelete", null, 0, 1, 2, null, null },
                    { 1102L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "新增", 1100L, "Identity.Role.Create", null, 0, 1, 2, null, null },
                    { 1103L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "编辑", 1100L, "Identity.Role.Update", null, 0, 1, 2, null, null },
                    { 1104L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "删除", 1100L, "Identity.Role.Delete", null, 0, 1, 2, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 201L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 202L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 203L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1002L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1003L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1004L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1102L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1103L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1104L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 200L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1001L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1100L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1000L);
        }
    }
}
