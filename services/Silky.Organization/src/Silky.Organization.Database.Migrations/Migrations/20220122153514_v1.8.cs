using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Organization.Database.Migrations.Migrations
{
    public partial class v18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Organizations");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "2a778c61-538b-40da-8c4f-228fa5533ff1");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "16a16784-b089-41f0-8c7c-71be6f77aa22");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "6cfd35cc-ad85-4375-8466-1c23c55c7b46");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "a78b0787-c847-4e18-b595-cbd337b40347");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ConcurrencyStamp",
                value: "2c40a87a-3235-4ed4-b554-98c5819bd785");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ConcurrencyStamp",
                value: "a468f943-78bc-418e-becc-685ec94e06d7");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Organizations",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "silky.hero", "42dab193-fb45-42f6-9e7c-eedd2c9dd9e7" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "silky.hero.dev", "e9ce91b7-770c-459b-8751-f8986b0dc3bf" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "silky.hero.dev.services", "1376737b-0d6d-462d-a28b-c314c76332e2" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "silky.hero.dev.front", "f0695790-7154-45a8-b402-ebe84d1ecad1" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "silky.hero.test", "e723f5b6-3155-47c8-b3b7-488b7697854a" });

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Code", "ConcurrencyStamp" },
                values: new object[] { "silky.hero.product", "ab444603-6d67-4af5-b1ed-c532a991a237" });
        }
    }
}
