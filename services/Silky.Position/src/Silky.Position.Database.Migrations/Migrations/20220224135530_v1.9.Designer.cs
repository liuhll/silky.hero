﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Silky.Position.EntityFrameworkCore.DbContexts;

#nullable disable

namespace Silky.Position.Database.Migrations.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20220224135530_v1.9")]
    partial class v19
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Silky.Position.Domain.Position", b =>
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

                    b.Property<long?>("DeletedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("DeletedBy");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeletedTime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<bool>("IsStatic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("IsStatic");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("Name");

                    b.Property<string>("Remark")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)")
                        .HasColumnName("Remark");

                    b.Property<int>("Sort")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("Sort");

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

                    b.ToTable("Positions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ConcurrencyStamp = "c8c35735-1ce4-429f-a55f-d2b040ef8e8c",
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = false,
                            Name = "总经理",
                            Sort = 99,
                            Status = 0,
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            ConcurrencyStamp = "db187c06-b626-4b11-af7a-cbd7bb5af9bc",
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = false,
                            Name = "技术总监",
                            Sort = 98,
                            Status = 0,
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            ConcurrencyStamp = "963aaf9c-b939-4fc5-91b6-7b5b7b44d363",
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = false,
                            Name = "后台组长",
                            Sort = 97,
                            Status = 0,
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            ConcurrencyStamp = "f6578d39-9566-4cd7-8013-b794e51d9c20",
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            IsStatic = false,
                            Name = "前台组长",
                            Sort = 96,
                            Status = 0,
                            TenantId = 1L
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
