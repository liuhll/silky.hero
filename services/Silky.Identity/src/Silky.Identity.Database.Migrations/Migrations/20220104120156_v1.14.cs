using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRoleMenu_Roles_RoleId",
                table: "IdentityRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRoleOrganizations_Roles_RoleId",
                table: "IdentityRoleOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityRoleOrganizations",
                table: "IdentityRoleOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityRoleMenu",
                table: "IdentityRoleMenu");

            migrationBuilder.RenameTable(
                name: "IdentityRoleOrganizations",
                newName: "RoleOrganizations");

            migrationBuilder.RenameTable(
                name: "IdentityRoleMenu",
                newName: "RoleMenus");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityRoleOrganizations_RoleId",
                table: "RoleOrganizations",
                newName: "IX_RoleOrganizations_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityRoleMenu_RoleId",
                table: "RoleMenus",
                newName: "IX_RoleMenus_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleOrganizations",
                table: "RoleOrganizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleMenus",
                table: "RoleMenus",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63d7423a-e677-443d-bb25-714e2fa4fd85", "AQAAAAEAACcQAAAAEIV36tN9sEaG5nylxMl6jYsefV0GgjY6wJVFS87TZyRtyZ30fW1zgt/iOFlws3MTMQ==", "7516cd69-b9a4-4cea-a215-66d8645af40c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c57ccda-7c03-44ec-8e01-46a70e43d17d", "AQAAAAEAACcQAAAAEH9HQqMZoi3seqPHjJ6iCQVz3y7PzjuWWoMX4zwDMrQrl3IjotB9mbDY2Gy+F+I9ew==", "627798fa-877e-4ec7-9bc5-4691229fd65a" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenus_Roles_RoleId",
                table: "RoleMenus",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleOrganizations_Roles_RoleId",
                table: "RoleOrganizations",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenus_Roles_RoleId",
                table: "RoleMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleOrganizations_Roles_RoleId",
                table: "RoleOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleOrganizations",
                table: "RoleOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleMenus",
                table: "RoleMenus");

            migrationBuilder.RenameTable(
                name: "RoleOrganizations",
                newName: "IdentityRoleOrganizations");

            migrationBuilder.RenameTable(
                name: "RoleMenus",
                newName: "IdentityRoleMenu");

            migrationBuilder.RenameIndex(
                name: "IX_RoleOrganizations_RoleId",
                table: "IdentityRoleOrganizations",
                newName: "IX_IdentityRoleOrganizations_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleMenus_RoleId",
                table: "IdentityRoleMenu",
                newName: "IX_IdentityRoleMenu_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityRoleOrganizations",
                table: "IdentityRoleOrganizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityRoleMenu",
                table: "IdentityRoleMenu",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef7e2e81-317e-4ac7-8360-b02df6acd938", "AQAAAAEAACcQAAAAEHLg0UFJFnd17iIFq7a++80dpzHiQ5T2qQ5fWKVzOEL0lBZhHPmYuylbCWGKG7/8hQ==", "981f1c9b-13f3-4c94-8603-5b12328aec8a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dde5e61b-bdd6-4831-888c-b3edf37d3c3d", "AQAAAAEAACcQAAAAEGT/Upod1ihYIWxFPQc+qnHQyx52MXgueaX1/fdtYpV/5H4ce8cnP4Fzs90gAfL6AA==", "977f0d5c-ace7-40df-b773-c4e49f243d0d" });

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleMenu_Roles_RoleId",
                table: "IdentityRoleMenu",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleOrganizations_Roles_RoleId",
                table: "IdentityRoleOrganizations",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
