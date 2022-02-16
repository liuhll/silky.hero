﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Silky.Identity.EntityFrameworkCore.DbContexts;

#nullable disable

namespace Silky.Identity.Database.Migrations.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    [Migration("20220216115738_v1.18")]
    partial class v118
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Silky.Identity.Domain.IdentityClaimType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("DeletedBy");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeletedTime");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<bool>("IsStatic")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Regex")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("RegexDescription")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<bool>("Required")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.Property<int>("ValueType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ClaimTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = true,
                            Name = "name",
                            Required = false,
                            TenantId = 1L,
                            ValueType = 0
                        },
                        new
                        {
                            Id = 2L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = true,
                            Name = "email",
                            Required = false,
                            TenantId = 1L,
                            ValueType = 0
                        },
                        new
                        {
                            Id = 3L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = true,
                            Name = "sex",
                            Required = false,
                            TenantId = 1L,
                            ValueType = 0
                        },
                        new
                        {
                            Id = 4L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = true,
                            Name = "mobilePhone",
                            Required = false,
                            TenantId = 1L,
                            ValueType = 0
                        },
                        new
                        {
                            Id = 5L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = true,
                            Name = "telPhone",
                            Required = false,
                            TenantId = 1L,
                            ValueType = 0
                        },
                        new
                        {
                            Id = 6L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = true,
                            Name = "surname",
                            Required = false,
                            TenantId = 1L,
                            ValueType = 0
                        },
                        new
                        {
                            Id = 7L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = true,
                            Name = "realName",
                            Required = false,
                            TenantId = 1L,
                            ValueType = 0
                        },
                        new
                        {
                            Id = 8L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = true,
                            Name = "jobNumber",
                            Required = false,
                            TenantId = 1L,
                            ValueType = 0
                        });
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<int>("DataRange")
                        .HasColumnType("int")
                        .HasColumnName("DataRange");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("DeletedBy");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeletedTime");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDefault");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsPublic");

                    b.Property<bool>("IsStatic")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsStatic");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext")
                        .HasColumnName("Remark");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("Status");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DataRange = 1,
                            IsDefault = false,
                            IsDeleted = false,
                            IsPublic = false,
                            IsStatic = false,
                            Name = "admin",
                            NormalizedName = "ADMIN",
                            RealName = "管理员",
                            Sort = 0,
                            Status = 1,
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DataRange = 1,
                            IsDefault = false,
                            IsDeleted = false,
                            IsPublic = false,
                            IsStatic = false,
                            Name = "normal",
                            NormalizedName = "NORMAL",
                            RealName = "一般角色",
                            Sort = 0,
                            Status = 1,
                            TenantId = 1L
                        });
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityRoleClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityRoleMenu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("DeletedBy");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeletedTime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<long>("MenuId")
                        .HasColumnType("bigint")
                        .HasColumnName("MenuId");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("RoleId");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleMenus", (string)null);
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityRoleOrganization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("DeletedBy");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeletedTime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint")
                        .HasColumnName("OrganizationId");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("RoleId");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleOrganizations", (string)null);
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("AccessFailedCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("AccessFailedCount");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("DeletedBy");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeletedTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("Email");

                    b.Property<bool>("EmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("EmailConfirmed");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("JobNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("LockoutEnd");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)")
                        .HasColumnName("MobilePhone");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("NormalizedEmail");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("NormalizedUserName");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("PasswordHash");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("PhoneNumberConfirmed");

                    b.Property<string>("RealName")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("RealName");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("SecurityStamp");

                    b.Property<int?>("Sex")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("Surname");

                    b.Property<string>("TelPhone")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)")
                        .HasColumnName("TelPhone");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("NormalizedEmail");

                    b.HasIndex("NormalizedUserName");

                    b.HasIndex("UserName");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccessFailedCount = 0,
                            BirthDay = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "706b9226-a4ae-45a1-adde-e4e9fa0e7372",
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "admin@silky.com",
                            EmailConfirmed = false,
                            IsActive = true,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            MobilePhone = "13111111111",
                            NormalizedEmail = "ADMIN@SILKY.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEGFI3Dretz0XkW/MVyzxLoHNSqQhiY/BmpAvOCQb9a2ndduNI3otDIJFvyxDRq7Ihw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9365083f-13da-4ab3-8926-00fe3cd9a1a0",
                            TenantId = 1L,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2L,
                            AccessFailedCount = 0,
                            BirthDay = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "41b5101a-c27c-4b0f-ad91-893b77c68c31",
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "liuhll@silky.com",
                            EmailConfirmed = false,
                            IsActive = true,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            MobilePhone = "13111111112",
                            NormalizedEmail = "LIUHLL@SILKY.COM",
                            NormalizedUserName = "LIUHLL",
                            PasswordHash = "AQAAAAEAACcQAAAAENJYnIhJidHprudo0T8Bg+SvDoZkHyfxHFhY/kaFUQlITvV055vgRgTZKRykRKcadQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ca9e1ec7-c91b-4f85-8cfb-53d847e020ea",
                            TenantId = 1L,
                            UserName = "liuhll"
                        });
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUserClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUserLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasMaxLength(196)
                        .HasColumnType("varchar(196)");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("RoleId", "UserId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Silky.Identity.Domain.UserSubsidiary", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreatedTime");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint")
                        .HasColumnName("OrganizationId");

                    b.Property<long>("PositionId")
                        .HasColumnType("bigint")
                        .HasColumnName("PositionId");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantId");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSubsidiaries", (string)null);
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityRoleClaim", b =>
                {
                    b.HasOne("Silky.Identity.Domain.IdentityRole", null)
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityRoleMenu", b =>
                {
                    b.HasOne("Silky.Identity.Domain.IdentityRole", null)
                        .WithMany("Menus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityRoleOrganization", b =>
                {
                    b.HasOne("Silky.Identity.Domain.IdentityRole", null)
                        .WithMany("CustomOrganizationDataRanges")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUserClaim", b =>
                {
                    b.HasOne("Silky.Identity.Domain.IdentityUser", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUserLogin", b =>
                {
                    b.HasOne("Silky.Identity.Domain.IdentityUser", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUserRole", b =>
                {
                    b.HasOne("Silky.Identity.Domain.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Silky.Identity.Domain.IdentityUser", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUserToken", b =>
                {
                    b.HasOne("Silky.Identity.Domain.IdentityUser", null)
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Silky.Identity.Domain.UserSubsidiary", b =>
                {
                    b.HasOne("Silky.Identity.Domain.IdentityUser", null)
                        .WithMany("UserSubsidiaries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityRole", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("CustomOrganizationDataRanges");

                    b.Navigation("Menus");
                });

            modelBuilder.Entity("Silky.Identity.Domain.IdentityUser", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("Roles");

                    b.Navigation("Tokens");

                    b.Navigation("UserSubsidiaries");
                });
#pragma warning restore 612, 618
        }
    }
}
