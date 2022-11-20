using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClaimTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Required = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsStatic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Regex = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegexDescription = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValueType = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RealName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsStatic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPublic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Remark = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataRange = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RealName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDay = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelPhone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobilePhone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    SecurityStamp = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    ClaimType = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleMenus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleOrganizations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleOrganizations_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    ClaimType = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(196)", maxLength: 196, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserSubsidiaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsLeader = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubsidiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubsidiaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ClaimTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "IsDeleted", "IsStatic", "Name", "Regex", "RegexDescription", "Required", "TenantId", "UpdatedBy", "UpdatedTime", "ValueType" },
                values: new object[,]
                {
                    { 1L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "name", null, null, false, 1L, null, null, 0 },
                    { 2L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "email", null, null, false, 1L, null, null, 0 },
                    { 3L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "sex", null, null, false, 1L, null, null, 0 },
                    { 4L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "mobilePhone", null, null, false, 1L, null, null, 0 },
                    { 5L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "telPhone", null, null, false, 1L, null, null, 0 },
                    { 6L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "surname", null, null, false, 1L, null, null, 0 },
                    { 7L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "realName", null, null, false, 1L, null, null, 0 },
                    { 8L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, true, "jobNumber", null, null, false, 1L, null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DataRange", "DeletedBy", "DeletedTime", "IsDefault", "IsDeleted", "IsPublic", "IsStatic", "Name", "NormalizedName", "RealName", "Remark", "Sort", "Status", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0, null, null, false, false, true, true, "admin", "ADMIN", "管理员", null, 0, 1, 1L, null, null },
                    { 2L, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, null, null, true, false, true, true, "normal", "NORMAL", "普通用户", null, 0, 1, 1L, null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDay", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "IsDeleted", "JobNumber", "LockoutEnd", "MobilePhone", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "RealName", "SecurityStamp", "Sex", "Status", "Surname", "TelPhone", "TenantId", "UpdatedBy", "UpdatedTime", "UserName" },
                values: new object[,]
                {
                    { 1L, null, "221d680a-f5d3-4029-b39c-1fd5c93b1e26", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@silky.com", false, "0001", null, "13111111111", "ADMIN@SILKY.COM", "ADMIN", "AQAAAAEAACcQAAAAEPVWF3xCiGkcSdDAXSSHZ3/2wdRM265zo4Casp6GnTXajewnYo26jRAQFsyRhyvjDA==", "管理员", "df7b0045-87ed-40a0-96d6-84ed26661eea", null, 1, null, null, 1L, null, null, "admin" },
                    { 2L, null, "6f24a12f-857a-45ff-af39-28609bc2d8fa", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "liuhll@silky.com", false, "0002", null, "13111111112", "LIUHLL@SILKY.COM", "LIUHLL", "AQAAAAEAACcQAAAAEFtFi0/YJStx+MXeOSsCx+gA9vB1kotyil+SFTOmVnPXZ9PjvN5W0HGiUykqkLtAgQ==", "Liuhll", "3bcdfa02-b8f1-4439-b008-4e24fcfa9509", null, 1, null, null, 1L, null, null, "liuhll" },
                    { 3L, null, "45e67529-2622-48bf-8de5-a41aae20a1a0", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "lanchong@silky.com", false, "0003", null, "13111111113", "LANCHONG@SILKY.COM", "LANCHONG", "AQAAAAEAACcQAAAAEMwr6wsxQoZpUg+8nLuztee2CYM/WmMS3nnkgidUq47Ts6ZmsIZzkYHuWYIWSsZrng==", "懒虫", "7b637054-6fa1-47cd-9768-0471665643ce", null, 1, null, null, 1L, null, null, "lanchong" }
                });

            migrationBuilder.InsertData(
                table: "RoleMenus",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "IsDeleted", "MenuId", "RoleId", "TenantId", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2918), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1L, 1L, 1L, null, null },
                    { 2L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2942), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2L, 1L, 1L, null, null },
                    { 3L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2949), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3L, 1L, 1L, null, null },
                    { 4L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2950), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 4L, 1L, 1L, null, null },
                    { 5L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2951), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 5L, 1L, 1L, null, null },
                    { 6L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2955), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 6L, 1L, 1L, null, null },
                    { 7L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2957), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 7L, 1L, 1L, null, null },
                    { 8L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2958), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 8L, 1L, 1L, null, null },
                    { 9L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2959), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 9L, 1L, 1L, null, null },
                    { 10L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2961), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 10L, 1L, 1L, null, null },
                    { 11L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2962), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 11L, 1L, 1L, null, null },
                    { 12L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2964), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 100L, 1L, 1L, null, null },
                    { 13L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2965), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 101L, 1L, 1L, null, null },
                    { 14L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2966), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 102L, 1L, 1L, null, null },
                    { 15L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2967), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 103L, 1L, 1L, null, null },
                    { 16L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2968), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 104L, 1L, 1L, null, null },
                    { 17L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2969), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 105L, 1L, 1L, null, null },
                    { 18L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2971), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 106L, 1L, 1L, null, null },
                    { 19L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2972), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 107L, 1L, 1L, null, null },
                    { 20L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2973), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 108L, 1L, 1L, null, null },
                    { 21L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2974), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 200L, 1L, 1L, null, null },
                    { 22L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2975), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 201L, 1L, 1L, null, null },
                    { 23L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2976), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 202L, 1L, 1L, null, null },
                    { 24L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2977), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 203L, 1L, 1L, null, null },
                    { 25L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2978), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 204L, 1L, 1L, null, null },
                    { 26L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2979), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 205L, 1L, 1L, null, null },
                    { 27L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2980), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1001L, 1L, 1L, null, null },
                    { 28L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2982), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1002L, 1L, 1L, null, null },
                    { 29L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2983), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1003L, 1L, 1L, null, null },
                    { 30L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2984), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1004L, 1L, 1L, null, null },
                    { 31L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2985), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1005L, 1L, 1L, null, null },
                    { 32L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2986), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1006L, 1L, 1L, null, null },
                    { 33L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2987), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1100L, 1L, 1L, null, null },
                    { 34L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2988), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1102L, 1L, 1L, null, null },
                    { 35L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2990), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1103L, 1L, 1L, null, null },
                    { 36L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2991), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1104L, 1L, 1L, null, null },
                    { 37L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2992), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1105L, 1L, 1L, null, null },
                    { 38L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2993), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1106L, 1L, 1L, null, null },
                    { 39L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2994), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1107L, 1L, 1L, null, null },
                    { 40L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2995), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 1108L, 1L, 1L, null, null },
                    { 41L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2996), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2100L, 1L, 1L, null, null },
                    { 42L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2997), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2101L, 1L, 1L, null, null },
                    { 43L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(2998), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2102L, 1L, 1L, null, null },
                    { 44L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3003), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2200L, 1L, 1L, null, null },
                    { 45L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3004), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2201L, 1L, 1L, null, null },
                    { 46L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3005), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2202L, 1L, 1L, null, null },
                    { 47L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3006), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2203L, 1L, 1L, null, null },
                    { 48L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3007), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2300L, 1L, 1L, null, null },
                    { 49L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3008), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2301L, 1L, 1L, null, null },
                    { 50L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3009), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2400L, 1L, 1L, null, null },
                    { 51L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3010), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2401L, 1L, 1L, null, null },
                    { 52L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3011), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2402L, 1L, 1L, null, null },
                    { 53L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3012), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2403L, 1L, 1L, null, null },
                    { 54L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3013), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2404L, 1L, 1L, null, null },
                    { 55L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3014), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2405L, 1L, 1L, null, null },
                    { 56L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3015), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2406L, 1L, 1L, null, null },
                    { 57L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3016), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2407L, 1L, 1L, null, null },
                    { 58L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3017), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2501L, 1L, 1L, null, null },
                    { 59L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3018), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2502L, 1L, 1L, null, null },
                    { 60L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3019), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2503L, 1L, 1L, null, null },
                    { 61L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3020), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2504L, 1L, 1L, null, null },
                    { 62L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3021), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2505L, 1L, 1L, null, null },
                    { 63L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3022), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2506L, 1L, 1L, null, null },
                    { 64L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3023), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2507L, 1L, 1L, null, null },
                    { 65L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3024), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3000L, 1L, 1L, null, null },
                    { 66L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3026), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3001L, 1L, 1L, null, null },
                    { 67L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3027), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3002L, 1L, 1L, null, null },
                    { 68L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3028), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3003L, 1L, 1L, null, null },
                    { 69L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3029), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3004L, 1L, 1L, null, null },
                    { 70L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3030), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3005L, 1L, 1L, null, null },
                    { 71L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3031), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3006L, 1L, 1L, null, null },
                    { 72L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3032), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3007L, 1L, 1L, null, null },
                    { 73L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3033), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 3008L, 1L, 1L, null, null },
                    { 74L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3034), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 10000L, 1L, 1L, null, null },
                    { 75L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3035), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 10001L, 1L, 1L, null, null },
                    { 76L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3036), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 10002L, 1L, 1L, null, null },
                    { 77L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3037), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 10003L, 1L, 1L, null, null },
                    { 78L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3038), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 10004L, 1L, 1L, null, null },
                    { 10000L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3044), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2L, 2L, 1L, null, null },
                    { 10001L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3046), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 10L, 2L, 1L, null, null },
                    { 10002L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3047), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 11L, 2L, 1L, null, null },
                    { 10003L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3048), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2100L, 2L, 1L, null, null },
                    { 10004L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3052), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2101L, 2L, 1L, null, null },
                    { 10005L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 316, DateTimeKind.Unspecified).AddTicks(3054), new TimeSpan(0, 8, 0, 0, 0)), null, null, false, 2102L, 2L, 1L, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "RoleId", "TenantId", "UpdatedBy", "UpdatedTime", "UserId" },
                values: new object[,]
                {
                    { 1L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 339, DateTimeKind.Unspecified).AddTicks(4210), new TimeSpan(0, 8, 0, 0, 0)), 1L, 1L, null, null, 1L },
                    { 2L, null, new DateTimeOffset(new DateTime(2022, 11, 20, 14, 33, 46, 339, DateTimeKind.Unspecified).AddTicks(4236), new TimeSpan(0, 8, 0, 0, 0)), 2L, 1L, null, null, 2L }
                });

            migrationBuilder.InsertData(
                table: "UserSubsidiaries",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "OrganizationId", "PositionId", "TenantId", "UpdatedBy", "UpdatedTime", "UserId" },
                values: new object[,]
                {
                    { 1L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1L, 1L, 1L, null, null, 1L },
                    { 2L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2L, 2L, 1L, null, null, 2L },
                    { 3L, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3L, 3L, 1L, null, null, 3L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_RoleId",
                table: "RoleMenus",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleOrganizations_RoleId",
                table: "RoleOrganizations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_NormalizedName",
                table: "Roles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_LoginProvider_ProviderKey",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId_UserId",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NormalizedEmail",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NormalizedUserName",
                table: "Users",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubsidiaries_UserId",
                table: "UserSubsidiaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimTypes");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "RoleMenus");

            migrationBuilder.DropTable(
                name: "RoleOrganizations");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserSubsidiaries");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
