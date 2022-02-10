using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Permission.Database.Migrations.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExternalLinkType",
                table: "Menus",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalLinkType",
                table: "Menus");
        }
    }
}
