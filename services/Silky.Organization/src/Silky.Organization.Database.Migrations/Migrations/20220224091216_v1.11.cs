using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Organization.Database.Migrations.Migrations
{
    public partial class v111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
