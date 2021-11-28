using Microsoft.EntityFrameworkCore.Migrations;

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "UserTokens",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "UserTokens",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "UserSubsidiaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "UserSubsidiaries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "UserRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "UserRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "UserLogins",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "UserLogins",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "UserClaims",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "UserClaims",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "RoleClaims",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "RoleClaims",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserSubsidiaries");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserSubsidiaries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RoleClaims");
        }
    }
}
