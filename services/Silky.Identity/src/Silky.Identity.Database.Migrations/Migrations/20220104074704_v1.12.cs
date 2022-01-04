using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataRange",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IdentityRoleOrganizations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    IdentityRoleId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleOrganizations_Roles_IdentityRoleId",
                        column: x => x.IdentityRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRoleOrganizations");

            migrationBuilder.DropColumn(
                name: "DataRange",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3cb7f38b-5c23-47ab-8ba5-78e8f44df04e", "AQAAAAEAACcQAAAAEEDIQA96XFvj6oiVLnnDvnnzS5/1yIn1JKDAgD+xTilGAAfhrhVyUp7VOsCtUqYWig==", "12d6bc77-4ab2-4bed-acbb-a47f780fa707" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e20325b-92a5-4adc-a60f-b791f7b7f003", "AQAAAAEAACcQAAAAEIYX6z5mqU/z0UkTyhzTsqSx0txo0lIcxCUM0Ys5dNySVWr8skTaDYkdxeSBu+cOUg==", "ab3daef9-f5f0-4508-a6ee-25cf4f2f8a9b" });
        }
    }
}
