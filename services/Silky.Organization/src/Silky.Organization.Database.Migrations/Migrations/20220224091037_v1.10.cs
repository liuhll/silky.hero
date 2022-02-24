using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Organization.Database.Migrations.Migrations
{
    public partial class v110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationPositions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationPositions_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "ec68d900-9a80-46e4-b789-bf477f947968");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "37a5c2b4-5fa8-4dbd-947d-59ef7f1d93e3");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "e7f9a2a4-7bba-4bb5-a5de-874fdecfc52a");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "c70a132c-783c-4864-a3ea-7ca6d0789176");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ConcurrencyStamp",
                value: "cdb855ef-8d57-4b17-9d94-212a8ae2f048");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ConcurrencyStamp",
                value: "354a87ea-c9bc-4a76-a4f7-78c5bdf71625");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRoles_OrganizationId",
                table: "OrganizationRoles",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPositions_OrganizationId",
                table: "OrganizationPositions",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationRoles_Organizations_OrganizationId",
                table: "OrganizationRoles",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationRoles_Organizations_OrganizationId",
                table: "OrganizationRoles");

            migrationBuilder.DropTable(
                name: "OrganizationPositions");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationRoles_OrganizationId",
                table: "OrganizationRoles");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "b26520d8-0d6e-4b83-8147-d533a2bba039");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "37557f9d-3e19-4265-aec1-05e36c351abf");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "2d02f18e-2497-41a1-a0d5-23e4b91bbb0d");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "8666a136-6957-4bb9-b5bb-1216e4be81de");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ConcurrencyStamp",
                value: "e18e6d58-4954-42ea-bdc8-4cd48e42b98c");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ConcurrencyStamp",
                value: "396294b1-658a-4eb0-a126-daf474ebd1c0");
        }
    }
}
