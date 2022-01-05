using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Permission.Database.Migrations.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Cache", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 1105L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "授权菜单", 1100L, "Identity.Role.SetMenus", null, 0, 1, 2, null, null });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Cache", "Component", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Display", "ExternalLink", "Icon", "IsDeleted", "Name", "ParentId", "PermissionCode", "RoutePath", "Sort", "Status", "Type", "UpdatedBy", "UpdatedTime" },
                values: new object[] { 1106L, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, null, null, false, "授权数据", 1100L, "Identity.Role.SetMenus", null, 0, 1, 2, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1105L);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1106L);
        }
    }
}
