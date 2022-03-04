using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Saas.Database.Migrations.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Code",
                value: "EnabledSilkyDashboard");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Code",
                value: "EnabledMenuManage");
        }
    }
}
