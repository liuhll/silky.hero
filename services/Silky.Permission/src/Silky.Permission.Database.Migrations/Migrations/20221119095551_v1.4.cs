using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Permission.Database.Migrations.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Component", "Display", "ExternalLink", "Icon", "RoutePath", "Sort" },
                values: new object[] { "LAYOUT", true, false, "ion:key-outline", "/authorization", 1000 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Component", "Display", "ExternalLink", "KeepAlive", "PermissionCode", "RoutePath" },
                values: new object[] { "/authorization/user/index", true, false, true, null, "/authorization/user" });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Sort",
                value: 1000);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Sort",
                value: 999);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Sort",
                value: 998);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Sort",
                value: 997);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Sort",
                value: 996);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Sort",
                value: 995);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Sort",
                value: 994);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 100L,
                columns: new[] { "Component", "Display", "ExternalLink", "PermissionCode", "RoutePath", "Sort" },
                values: new object[] { "/authorization/organization/index", true, false, null, "/authorization/organization", 1000 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 101L,
                column: "Sort",
                value: 999);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 102L,
                column: "Sort",
                value: 998);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 103L,
                column: "Sort",
                value: 997);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 200L,
                columns: new[] { "Component", "Display", "ExternalLink", "Name", "PermissionCode", "RoutePath", "Sort" },
                values: new object[] { "/authorization/position/index", true, false, "岗位管理", null, "/authorization/position", 1000 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 201L,
                column: "Sort",
                value: 999);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 202L,
                column: "Sort",
                value: 998);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 203L,
                column: "Sort",
                value: 997);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1001L,
                columns: new[] { "Component", "Display", "ExternalLink", "PermissionCode", "RoutePath", "Sort" },
                values: new object[] { "/authorization/menu/index", true, false, null, "/authorization/menu", 1000 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "Sort",
                value: 999);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1003L,
                column: "Sort",
                value: 998);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1004L,
                columns: new[] { "PermissionCode", "Sort" },
                values: new object[] { "Permission.Menu.Delete", 997 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1100L,
                columns: new[] { "Component", "Display", "ExternalLink", "PermissionCode", "RoutePath", "Sort" },
                values: new object[] { "/authorization/role/index", true, false, null, "/authorization/role", 1000 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1102L,
                column: "Sort",
                value: 999);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1103L,
                column: "Sort",
                value: 998);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1104L,
                column: "Sort",
                value: 997);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1105L,
                column: "Sort",
                value: 996);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1106L,
                column: "Sort",
                value: 995);

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 10L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 2L, "Identity.User.Search", null, 993, 0, 2, null, null },
                    { 11L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 2L, "Identity.User.LookDetail", null, 992, 0, 2, null, null },
                    { 104L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增成员", 100L, "Organization.AddUsers", null, 996, 0, 2, null, null },
                    { 105L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "移除成员", 100L, "Organization.RemoveUser", null, 995, 0, 2, null, null },
                    { 106L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "分配角色", 100L, "Organization.AllocationRole", null, 994, 0, 2, null, null },
                    { 107L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "分配职位", 100L, "Organization.AllocationPosition", null, 993, 0, 2, null, null },
                    { 108L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 100L, "Organization.LookDetail", null, 994, 0, 2, null, null },
                    { 204L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 200L, "Position.Search", null, 996, 0, 2, null, null },
                    { 205L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 200L, "Position.LookDetail", null, 995, 0, 2, null, null },
                    { 1005L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 1001L, "Permission.Menu.Search", null, 996, 0, 2, null, null },
                    { 1006L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 1001L, "Permission.Menu.LookDetail", null, 995, 0, 2, null, null },
                    { 1107L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 1100L, "Identity.Role.Search", null, 994, 0, 2, null, null },
                    { 1108L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 1100L, "Identity.Role.LookDetail", null, 993, 0, 2, null, null },
                    { 2100L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, "ion:grid-outline", false, null, "Dashboard", null, null, "/dashboard", 1001, 0, 0, null, null },
                    { 2200L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, "ant-design:audit-outlined", false, null, "日志管理", null, null, "/log", 999, 0, 0, null, null },
                    { 2300L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, "simple-icons:about-dot-me", false, null, "关于", null, null, "/about", -1000, 0, 0, null, null },
                    { 2400L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, "ant-design:team-outlined", false, null, "Saas", null, null, "/about", 997, 0, 0, null, null },
                    { 3000L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, "icon-park-outline:setting-config", false, null, "基础配置", null, null, "/basic-config", 996, 0, 0, null, null },
                    { 10000L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, "ion:tv-outline", false, null, "外部链接", null, null, "/externallink", 0, 0, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 2101L, "/dashboard/analysis/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, true, "分析页", 2100L, null, "/dashboard/analysis", 1000, 0, 1, null, null },
                    { 2102L, "/dashboard/workbench/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, true, "工作台", 2100L, null, "/dashboard/workbench", 999, 0, 1, null, null },
                    { 2201L, "/log-manage/audit/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, null, false, null, "审计日志", 2200L, null, "/log/audit", 998, 0, 1, null, null },
                    { 2202L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 2200L, "Log.AuditLogging.Search", null, 998, 0, 2, null, null },
                    { 2203L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 2200L, "Log.AuditLogging.LookDetail", null, 997, 0, 2, null, null },
                    { 2301L, "/about/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, null, false, null, "关于详情", 2300L, null, "/about", 997, 0, 1, null, null },
                    { 2401L, "/saas/tenant/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, null, false, null, "租户", 2400L, null, "/saas/tenant", 997, 0, 1, null, null },
                    { 2501L, "/saas/edition/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, null, false, null, "版本", 2400L, null, "/saas/edition", 990, 0, 1, null, null },
                    { 3001L, "/basic-config/dictionary/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, null, false, null, "字典管理", 3000L, null, "/basic-config/dictionary", 1000, 0, 1, null, null },
                    { 10001L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, null, false, null, "Github地址", 10000L, null, "https://github.com/liuhll/silky.hero", 996, 0, 1, null, null },
                    { 10002L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, null, false, null, "Swagger文档", 10000L, null, "https://localhost:5001/index.html", 995, 0, 1, null, null },
                    { 10003L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, null, false, null, "服务健康检查", 10000L, null, "https://localhost:5001/healthchecks-ui", 994, 0, 1, null, null },
                    { 10004L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, null, false, null, "微服务管理端", 10000L, null, "https://localhost:5001/dashboard/index.html", 993, 0, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 2402L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 2401L, "Tenant.Create", null, 996, 0, 2, null, null },
                    { 2403L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 2401L, "Tenant.Create", null, 995, 0, 2, null, null },
                    { 2404L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 2401L, "Tenant.Update", null, 994, 0, 2, null, null },
                    { 2405L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 2401L, "Tenant.Delete", null, 993, 0, 2, null, null },
                    { 2406L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 2401L, "Tenant.Search", null, 992, 0, 2, null, null },
                    { 2407L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 2401L, "Tenant.LookDetail", null, 991, 0, 2, null, null },
                    { 2502L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 2501L, "Edition.Create", null, 989, 0, 2, null, null },
                    { 2503L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 2501L, "Edition.Update", null, 988, 0, 2, null, null },
                    { 2504L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 2501L, "Edition.Delete", null, 987, 0, 2, null, null },
                    { 2505L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "设置功能", 2501L, "Edition.SetFeatures", null, 986, 0, 2, null, null },
                    { 2506L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 2501L, "Edition.Search", null, 985, 0, 2, null, null },
                    { 2507L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 2501L, "Edition.LookDetail", null, 984, 0, 2, null, null },
                    { 3002L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "字典项", 3001L, "BasicData.Dictionary.Item", null, 999, 0, 2, null, null },
                    { 3003L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 3001L, "BasicData.Dictionary.Type.Create", null, 997, 0, 2, null, null },
                    { 3004L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 3001L, "BasicData.Dictionary.Type.Update", null, 996, 0, 2, null, null },
                    { 3005L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 3001L, "BasicData.Dictionary.Type.Delete", null, 995, 0, 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 3006L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 3002L, "BasicData.Dictionary.Item.Create", null, 994, 0, 2, null, null });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 3007L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 3002L, "BasicData.Dictionary.Item.Update", null, 993, 0, 2, null, null });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 3008L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 3002L, "BasicData.Dictionary.Item.Delete", null, 992, 0, 2, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 204L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 205L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1005L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1006L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1107L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1108L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2101L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2102L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2201L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2202L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2203L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2301L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2402L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2403L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2404L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2405L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2406L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2407L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2502L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2503L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2504L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2505L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2506L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2507L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3003L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3004L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3005L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3006L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3007L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3008L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10001L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10002L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10003L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10004L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2100L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2200L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2300L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2401L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2501L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3002L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 10000L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2400L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3001L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3000L);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Component", "Display", "ExternalLink", "Icon", "RoutePath", "Sort" },
                values: new object[] { null, null, null, null, null, 998 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Component", "Display", "ExternalLink", "KeepAlive", "PermissionCode", "RoutePath" },
                values: new object[] { null, null, null, null, "Identity.User", null });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 100L,
                columns: new[] { "Component", "Display", "ExternalLink", "PermissionCode", "RoutePath", "Sort" },
                values: new object[] { null, null, null, "Organization.Default", null, 998 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 101L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 102L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 103L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 200L,
                columns: new[] { "Component", "Display", "ExternalLink", "Name", "PermissionCode", "RoutePath", "Sort" },
                values: new object[] { null, null, null, "职位管理", "Position.Default", null, 997 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 201L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 202L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 203L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1001L,
                columns: new[] { "Component", "Display", "ExternalLink", "PermissionCode", "RoutePath", "Sort" },
                values: new object[] { null, null, null, "Permission.Menu", null, 999 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1002L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1003L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1004L,
                columns: new[] { "PermissionCode", "Sort" },
                values: new object[] { "Permission.MenuDelete", 0 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1100L,
                columns: new[] { "Component", "Display", "ExternalLink", "PermissionCode", "RoutePath", "Sort" },
                values: new object[] { null, null, null, "Identity.Role", null, 998 });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1102L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1103L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1104L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1105L,
                column: "Sort",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1106L,
                column: "Sort",
                value: 0);
        }
    }
}
