using Microsoft.EntityFrameworkCore.Migrations;

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnits_Users_UserId",
                table: "OrganizationUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationUnits",
                table: "OrganizationUnits");

            migrationBuilder.RenameTable(
                name: "OrganizationUnits",
                newName: "UserSubsidiaries");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationUnits_UserId",
                table: "UserSubsidiaries",
                newName: "IX_UserSubsidiaries_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubsidiaries",
                table: "UserSubsidiaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubsidiaries_Users_UserId",
                table: "UserSubsidiaries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubsidiaries_Users_UserId",
                table: "UserSubsidiaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubsidiaries",
                table: "UserSubsidiaries");

            migrationBuilder.RenameTable(
                name: "UserSubsidiaries",
                newName: "OrganizationUnits");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubsidiaries_UserId",
                table: "OrganizationUnits",
                newName: "IX_OrganizationUnits_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationUnits",
                table: "OrganizationUnits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnits_Users_UserId",
                table: "OrganizationUnits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
