using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.BasicData.Database.Migrations.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DictionaryItems",
                newName: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "DictionaryItems",
                newName: "Name");
        }
    }
}
