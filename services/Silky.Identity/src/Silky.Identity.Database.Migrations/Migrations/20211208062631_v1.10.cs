using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UserClaims",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "RoleClaims",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f22e3cf-723a-4723-976d-782bdb9ccbf8", "AQAAAAEAACcQAAAAEIf1N7m0Fx9ihGlNQro55oF1xAR7kMDJLyTY1uGgFHzz9mUWA1jP32DwFSIHdjna1A==", "ba891b92-09ee-452f-ac64-2425c0790aea" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85524d61-8c65-4b93-aa61-680b986bc444", "AQAAAAEAACcQAAAAEP6R369FZIMth9/muce3nSoRLNASA4j/ZUgVxX2NofPvKOJw8BXTH8kPI5+ByOr/Ww==", "43eca817-7cc1-4898-ab79-2bac239b4deb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "UserTokens",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "JobNumber",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UserClaims",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "RoleClaims",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f9be60a-b660-4c45-b543-5263b049919c", "AQAAAAEAACcQAAAAEHnMndKow19uorgZ26RzEayk0PM8TbRfOX16Wmd847AHEOmVEeH51GRiQEVVEUIoPg==", "7b64fbde-0c19-4a04-bf55-d374d7d1c977" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acb6f8a9-63b6-41e7-9c68-2caff38aba52", "AQAAAAEAACcQAAAAEIFEWtMvDD6tfRsfcyR7wG7P6X+/N19D95R52laincUazfRs6OtJau1ZDxZnjouSGw==", "2613f59a-a4fd-41ed-8664-1fd4373a9e96" });
        }
    }
}
