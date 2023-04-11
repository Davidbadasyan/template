﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UM.Infrastructure.DbContexts;

#nullable disable

namespace UM.Infrastructure.Migrations
{
    [DbContext(typeof(UmContext))]
    [Migration("20230406135158_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UM.Domain.AggregatesModel.UserAggregate.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("last_name");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("modified_date")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int>("_roleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("_statusId")
                        .HasColumnType("int")
                        .HasColumnName("approval_status_id");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("_roleId");

                    b.HasIndex("_statusId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("UM.Domain.AggregatesModel.UserAggregate.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("user_roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "user"
                        },
                        new
                        {
                            Id = 1,
                            Name = "superadmin"
                        });
                });

            modelBuilder.Entity("UM.Domain.AggregatesModel.UserAggregate.UserStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("user_statuses", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Waiting for approval"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Deactivated"
                        });
                });

            modelBuilder.Entity("UM.Domain.AggregatesModel.UserAggregate.User", b =>
                {
                    b.HasOne("UM.Domain.AggregatesModel.UserAggregate.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("_roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UM.Domain.AggregatesModel.UserAggregate.UserStatus", "Status")
                        .WithMany()
                        .HasForeignKey("_statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}