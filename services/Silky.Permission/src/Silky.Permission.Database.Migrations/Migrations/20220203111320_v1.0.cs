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
                    Display = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    KeepAlive = table.Column<bool>(type: "tinyint(1)", nullable: true),
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
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 1L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "权限管理", null, null, null, 998, 0, 0, null, null });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 2L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "用户管理", 1L, "Identity.User", null, 999, 0, 1, null, null },
                    { 100L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "机构管理", 1L, "Organization.Default", null, 998, 0, 1, null, null },
                    { 200L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "职位管理", 1L, "Position.Default", null, 997, 0, 1, null, null },
                    { 1001L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "菜单管理", 1L, "Permission.Menu", null, 999, 0, 1, null, null },
                    { 1100L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "角色管理", 1L, "Identity.Role", null, 998, 0, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "KeepAlive", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 3L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "新增", 2L, "Identity.User.Create", null, 0, 0, 2, null, null },
                    { 4L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "编辑", 2L, "Identity.User.Update", null, 0, 0, 2, null, null },
                    { 5L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "删除", 2L, "Identity.User.Delete", null, 0, 0, 2, null, null },
                    { 6L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "锁定", 2L, "Identity.User.Lock", null, 0, 0, 2, null, null },
                    { 7L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "解锁", 2L, "Identity.User.UnLock", null, 0, 0, 2, null, null },
                    { 8L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "更新声明", 2L, "Identity.User.UpdateClaimTypes", null, 0, 0, 2, null, null },
                    { 9L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "授权角色", 2L, "Identity.User.SetRoles", null, 0, 0, 2, null, null },
                    { 101L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "新增", 100L, "Organization.Create", null, 0, 0, 2, null, null },
                    { 102L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "编辑", 100L, "Organization.Update", null, 0, 0, 2, null, null },
                    { 103L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "删除", 100L, "Organization.Delete", null, 0, 0, 2, null, null },
                    { 201L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "新增", 200L, "Position.Create", null, 0, 0, 2, null, null },
                    { 202L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "编辑", 200L, "Position.Update", null, 0, 0, 2, null, null },
                    { 203L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "删除", 200L, "Position.Delete", null, 0, 0, 2, null, null },
                    { 1002L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "新增", 1001L, "Permission.Menu.Create", null, 0, 0, 2, null, null },
                    { 1003L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "编辑", 1001L, "Permission.Menu.Update", null, 0, 0, 2, null, null },
                    { 1004L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "删除", 1001L, "Permission.MenuDelete", null, 0, 0, 2, null, null },
                    { 1102L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "新增", 1100L, "Identity.Role.Create", null, 0, 0, 2, null, null },
                    { 1103L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "编辑", 1100L, "Identity.Role.Update", null, 0, 0, 2, null, null },
                    { 1104L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "删除", 1100L, "Identity.Role.Delete", null, 0, 0, 2, null, null },
                    { 1105L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "授权菜单", 1100L, "Identity.Role.SetMenus", null, 0, 0, 2, null, null },
                    { 1106L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, null, "授权数据", 1100L, "Identity.Role.SetMenus", null, 0, 0, 2, null, null }
                });

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
