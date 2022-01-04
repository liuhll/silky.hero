using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRoleOrganizations_Roles_IdentityRoleId",
                table: "IdentityRoleOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_IdentityRoleOrganizations_IdentityRoleId",
                table: "IdentityRoleOrganizations");

            migrationBuilder.RenameColumn(
                name: "IdentityRoleId",
                table: "IdentityRoleOrganizations",
                newName: "DeletedBy");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedTime",
                table: "IdentityRoleOrganizations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "IdentityRoleOrganizations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "IdentityRoleMenu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleMenu_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRange",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataRange",
                value: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRoleOrganizations_RoleId",
                table: "IdentityRoleOrganizations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRoleMenu_RoleId",
                table: "IdentityRoleMenu",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleOrganizations_Roles_RoleId",
                table: "IdentityRoleOrganizations",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRoleOrganizations_Roles_RoleId",
                table: "IdentityRoleOrganizations");

            migrationBuilder.DropTable(
                name: "IdentityRoleMenu");

            migrationBuilder.DropIndex(
                name: "IX_IdentityRoleOrganizations_RoleId",
                table: "IdentityRoleOrganizations");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "IdentityRoleOrganizations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IdentityRoleOrganizations");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "IdentityRoleOrganizations",
                newName: "IdentityRoleId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRange",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataRange",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7df8f6a-acb5-44d7-852d-54702508ba6f", "AQAAAAEAACcQAAAAEMLw0rIFUi9waz5kH/qR1Vn2o58EjeGoOoL4m9VWVK4VaHqch77Y+eBOCYq1CQ16fg==", "e2a718c4-e574-411a-a484-e382bc564fbf" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3684bad7-29a0-41ce-8dde-57f42ca860fa", "AQAAAAEAACcQAAAAEGbhHt+M0MuwQw+ok/GzoGjtxdXJGFjDE8AMCdFzhOhuB9j/QLTI309WX2g0Bo5Kdw==", "4dadae6a-2f34-4380-a5be-1af88e10f7be" });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRoleOrganizations_IdentityRoleId",
                table: "IdentityRoleOrganizations",
                column: "IdentityRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleOrganizations_Roles_IdentityRoleId",
                table: "IdentityRoleOrganizations",
                column: "IdentityRoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
