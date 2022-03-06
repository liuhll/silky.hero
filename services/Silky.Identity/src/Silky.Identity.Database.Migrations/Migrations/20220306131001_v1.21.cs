using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLeader",
                table: "UserSubsidiaries",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c51f808-da76-42ab-9665-fdc841d6ec87", "AQAAAAEAACcQAAAAEHtVPBHORount+eac1xhbeZmN7eUg4Mgt3NrR/6V9TjQQSc6IhUeUUJ4EGRtWTG73A==", "9aaeb311-fad7-4983-a13e-17d621a321ea" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acb87008-ec1e-4040-94ce-6c1f8b8ca84f", "AQAAAAEAACcQAAAAEEGUwDZIpXwiQ3HPuRwr3Mit9VCjZF7wDA7779jKbyG1B3GMJ5KiRpDpbh8erb2HOg==", "3f810159-7ef7-4450-b39b-f241fc56f89b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLeader",
                table: "UserSubsidiaries");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68972256-4df6-4d05-a55c-525eab7c01e2", "AQAAAAEAACcQAAAAEGjXPT8+TYrfXtYplLwNx2G3s1Yi1REJCWuPmQj6LXnNW4rfqxaOiyH/Uq2opongcw==", "280de1cb-4724-4438-b90a-f5d7265febb3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9210ded5-77ef-4739-8ae2-ce34ce0be4f9", "AQAAAAEAACcQAAAAEKBMQcctobzCKh+OYa0PfHqc4tq6p0gfQNXTQRadspuh/pEJL5R4EqyqYxzp9mIZDg==", "04e7fb1d-731c-4e4b-9b1c-2eb1cd8e7043" });
        }
    }
}
