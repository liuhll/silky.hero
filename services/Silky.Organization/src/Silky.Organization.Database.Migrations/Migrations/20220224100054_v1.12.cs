using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Organization.Database.Migrations.Migrations
{
    public partial class v112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "453e2dc3-1f53-4569-898d-1921f056f0ee");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "9faffd89-61c2-4bfb-9c87-d6a44ea9655d");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "53b179af-17e0-40f8-aefb-aafa83e10bbe");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "2ee639ef-a240-4081-8bdb-4f1ecee598ba");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ConcurrencyStamp",
                value: "6b1ef995-dd72-4516-9f2a-ca468c279e42");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ConcurrencyStamp",
                value: "d1b47f77-d3bf-4745-9c25-9383de07c42b");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "026508bb-feb1-4e96-972b-1b7a02fdd7af");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "4e773015-de56-4374-8a57-19a2e2f7ccf4");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "36489d84-fe2e-4a03-a11f-3bba1f71a5da");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "49c871c9-cc47-46f7-a4f4-fe59d3e9e23c");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ConcurrencyStamp",
                value: "9e7d55cf-00bf-4850-811d-08da44be5c71");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ConcurrencyStamp",
                value: "3db43494-ba6d-4429-9e87-5eb415d7eb6a");
        }
    }
}
