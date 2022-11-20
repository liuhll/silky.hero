using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Permission.Database.Migrations.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermissionCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Icon = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    RoutePath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Component = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExternalLink = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    ExternalLinkType = table.Column<int>(type: "int", nullable: true),
                    Display = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    KeepAlive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    HideBreadcrumb = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    HideChildrenInMenu = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CurrentActiveMenu = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menus",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, "ion:key-outline", false, null, "权限管理", null, null, "/authorization", 1000, 1, 0, null, null },
                    { 2100L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, "ion:grid-outline", false, null, "Dashboard", null, null, "/dashboard", 1001, 1, 0, null, null },
                    { 2200L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, "ant-design:audit-outlined", false, null, "日志管理", null, null, "/log", 999, 1, 0, null, null },
                    { 2300L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, true, "simple-icons:about-dot-me", false, null, "关于", null, null, "/about", -1000, 1, 0, null, null },
                    { 2400L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, "ant-design:team-outlined", false, null, "Saas", null, null, "/saas", 997, 1, 0, null, null },
                    { 3000L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, "icon-park-outline:setting-config", false, null, "基础配置", null, null, "/basic-config", 996, 1, 0, null, null },
                    { 10000L, "LAYOUT", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, "ion:tv-outline", false, null, "外部链接", null, null, "/externallink", 0, 1, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 2L, "/authorization/user/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, true, "用户管理", 1L, null, "/authorization/user", 999, 1, 1, null, null },
                    { 100L, "/authorization/organization/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, null, "机构管理", 1L, null, "/authorization/organization", 1000, 1, 1, null, null },
                    { 200L, "/authorization/position/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, null, "岗位管理", 1L, null, "/authorization/position", 1000, 1, 1, null, null },
                    { 1001L, "/authorization/menu/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, null, "菜单管理", 1L, null, "/authorization/menu", 1000, 1, 1, null, null },
                    { 1100L, "/authorization/role/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, null, "角色管理", 1L, null, "/authorization/role", 1000, 1, 1, null, null },
                    { 2101L, "/dashboard/analysis/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, true, "分析页", 2100L, null, "/dashboard/analysis", 1000, 1, 1, null, null },
                    { 2102L, "/dashboard/workbench/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, true, "工作台", 2100L, null, "/dashboard/workbench", 999, 1, 1, null, null },
                    { 2201L, "/log-manage/audit/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, null, false, null, "审计日志", 2200L, null, "/log/audit", 998, 1, 1, null, null },
                    { 2202L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 2200L, "Log.AuditLogging.Search", null, 998, 1, 2, null, null },
                    { 2203L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 2200L, "Log.AuditLogging.LookDetail", null, 997, 1, 2, null, null },
                    { 2301L, "/sys/about/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, null, false, null, "关于详情", 2300L, null, "/about/index", 997, 1, 1, null, null },
                    { 2401L, "/saas/tenant/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, null, "租户", 2400L, null, "/saas/tenant", 997, 1, 1, null, null },
                    { 2501L, "/saas/edition/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, false, null, null, null, null, false, null, "版本", 2400L, null, "/saas/edition", 990, 1, 1, null, null },
                    { 3001L, "/basic-config/dictionary/index", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, false, null, null, null, null, false, null, "字典管理", 3000L, null, "/basic-config/dictionary", 1000, 1, 1, null, null },
                    { 10001L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, null, false, null, "Github地址", 10000L, null, "https://github.com/liuhll/silky.hero", 996, 1, 1, null, null },
                    { 10002L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, null, false, null, "Swagger文档", 10000L, null, "https://localhost:5001/index.html", 995, 1, 1, null, null },
                    { 10003L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, null, false, null, "服务健康检查", 10000L, null, "https://localhost:5001/healthchecks-ui", 994, 1, 1, null, null },
                    { 10004L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, true, true, null, null, null, null, false, null, "微服务管理端", 10000L, null, "https://localhost:5001/dashboard/index.html", 993, 1, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 3L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 2L, "Identity.User.Create", null, 1000, 1, 2, null, null },
                    { 4L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 2L, "Identity.User.Update", null, 999, 1, 2, null, null },
                    { 5L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 2L, "Identity.User.Delete", null, 998, 1, 2, null, null },
                    { 6L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "锁定", 2L, "Identity.User.Lock", null, 997, 1, 2, null, null },
                    { 7L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "解锁", 2L, "Identity.User.UnLock", null, 996, 1, 2, null, null },
                    { 8L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "更新声明", 2L, "Identity.User.UpdateClaimTypes", null, 995, 1, 2, null, null },
                    { 9L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "授权角色", 2L, "Identity.User.SetRoles", null, 994, 1, 2, null, null },
                    { 10L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 2L, "Identity.User.Search", null, 993, 1, 2, null, null },
                    { 11L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 2L, "Identity.User.LookDetail", null, 992, 1, 2, null, null },
                    { 101L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 100L, "Organization.Create", null, 999, 1, 2, null, null },
                    { 102L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 100L, "Organization.Update", null, 998, 1, 2, null, null },
                    { 103L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 100L, "Organization.Delete", null, 997, 1, 2, null, null },
                    { 104L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增成员", 100L, "Organization.AddUsers", null, 996, 1, 2, null, null },
                    { 105L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "移除成员", 100L, "Organization.RemoveUser", null, 995, 0, 2, null, null },
                    { 106L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "分配角色", 100L, "Organization.AllocationRole", null, 994, 1, 2, null, null },
                    { 107L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "分配职位", 100L, "Organization.AllocationPosition", null, 993, 1, 2, null, null },
                    { 108L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 100L, "Organization.LookDetail", null, 994, 1, 2, null, null },
                    { 201L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 200L, "Position.Create", null, 999, 1, 2, null, null },
                    { 202L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 200L, "Position.Update", null, 998, 1, 2, null, null },
                    { 203L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 200L, "Position.Delete", null, 997, 1, 2, null, null },
                    { 204L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 200L, "Position.Search", null, 996, 1, 2, null, null },
                    { 205L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 200L, "Position.LookDetail", null, 995, 1, 2, null, null },
                    { 1002L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 1001L, "Permission.Menu.Create", null, 999, 1, 2, null, null },
                    { 1003L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 1001L, "Permission.Menu.Update", null, 998, 1, 2, null, null },
                    { 1004L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 1001L, "Permission.Menu.Delete", null, 997, 1, 2, null, null },
                    { 1005L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 1001L, "Permission.Menu.Search", null, 996, 1, 2, null, null },
                    { 1006L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 1001L, "Permission.Menu.LookDetail", null, 995, 1, 2, null, null },
                    { 1102L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 1100L, "Identity.Role.Create", null, 999, 1, 2, null, null },
                    { 1103L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 1100L, "Identity.Role.Update", null, 998, 1, 2, null, null },
                    { 1104L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 1100L, "Identity.Role.Delete", null, 997, 1, 2, null, null },
                    { 1105L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "授权菜单", 1100L, "Identity.Role.SetMenus", null, 996, 1, 2, null, null },
                    { 1106L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "授权数据", 1100L, "Identity.Role.SetDataRange", null, 995, 1, 2, null, null },
                    { 1107L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 1100L, "Identity.Role.Search", null, 994, 1, 2, null, null },
                    { 1108L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 1100L, "Identity.Role.LookDetail", null, 993, 1, 2, null, null },
                    { 2402L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 2401L, "Tenant.Create", null, 996, 1, 2, null, null },
                    { 2403L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 2401L, "Tenant.Create", null, 995, 1, 2, null, null },
                    { 2404L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 2401L, "Tenant.Update", null, 994, 1, 2, null, null },
                    { 2405L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 2401L, "Tenant.Delete", null, 993, 1, 2, null, null },
                    { 2406L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 2401L, "Tenant.Search", null, 992, 1, 2, null, null },
                    { 2407L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 2401L, "Tenant.LookDetail", null, 991, 1, 2, null, null },
                    { 2502L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 2501L, "Edition.Create", null, 989, 1, 2, null, null },
                    { 2503L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 2501L, "Edition.Update", null, 988, 1, 2, null, null },
                    { 2504L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 2501L, "Edition.Delete", null, 987, 1, 2, null, null },
                    { 2505L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "设置功能", 2501L, "Edition.SetFeatures", null, 986, 1, 2, null, null },
                    { 2506L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "查询", 2501L, "Edition.Search", null, 985, 1, 2, null, null },
                    { 2507L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "详情", 2501L, "Edition.LookDetail", null, 984, 1, 2, null, null },
                    { 3002L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "字典项", 3001L, "BasicData.Dictionary.Item", null, 999, 1, 2, null, null },
                    { 3003L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 3001L, "BasicData.Dictionary.Type.Create", null, 997, 1, 2, null, null },
                    { 3004L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 3001L, "BasicData.Dictionary.Type.Update", null, 996, 1, 2, null, null },
                    { 3005L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 3001L, "BasicData.Dictionary.Type.Delete", null, 995, 1, 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 3006L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "新增", 3002L, "BasicData.Dictionary.Item.Create", null, 994, 1, 2, null, null });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 3007L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "编辑", 3002L, "BasicData.Dictionary.Item.Update", null, 993, 1, 2, null, null });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "CurrentActiveMenu", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "ExternalLinkType", "HideBreadcrumb", "HideChildrenInMenu", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 3008L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, null, null, null, null, false, null, "删除", 3002L, "BasicData.Dictionary.Item.Delete", null, 992, 1, 2, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
