using Microsoft.EntityFrameworkCore.Migrations;

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrganizations_Users_UserId",
                table: "UserOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOrganizations",
                table: "UserOrganizations");

            migrationBuilder.RenameTable(
                name: "UserOrganizations",
                newName: "UserSubsidiaries");

            migrationBuilder.RenameIndex(
                name: "IX_UserOrganizations_UserId",
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
                newName: "UserOrganizations");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubsidiaries_UserId",
                table: "UserOrganizations",
                newName: "IX_UserOrganizations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOrganizations",
                table: "UserOrganizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrganizations_Users_UserId",
                table: "UserOrganizations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
