﻿// <auto-generated />
using System;
using Common.Infrastructure.EventBus.IntegrationEventLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace UM.Infrastructure.Migrations.IntegrationEventLogger
{
    [DbContext(typeof(IntegrationEventLoggerContext))]
    partial class IntegrationEventLoggerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Common.Infrastructure.EventBus.IntegrationEventLogger.EventState", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("integration_event_states", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "notpublished"
                        },
                        new
                        {
                            Id = 2,
                            Name = "inprogress"
                        },
                        new
                        {
                            Id = 3,
                            Name = "published"
                        },
                        new
                        {
                            Id = 4,
                            Name = "publishedfailed"
                        });
                });

            modelBuilder.Entity("Common.Infrastructure.EventBus.IntegrationEventLogger.IntegrationEventLogEntry", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("event_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("creation_time");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("event_type_name");

                    b.Property<int>("TimesSent")
                        .HasColumnType("int")
                        .HasColumnName("times_sent");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("transaction_id");

                    b.Property<int>("_stateId")
                        .HasColumnType("int")
                        .HasColumnName("state_id");

                    b.HasKey("EventId");

                    b.HasIndex("_stateId");

                    b.ToTable("integration_event_logs", (string)null);
                });

            modelBuilder.Entity("Common.Infrastructure.EventBus.IntegrationEventLogger.IntegrationEventLogEntry", b =>
                {
                    b.HasOne("Common.Infrastructure.EventBus.IntegrationEventLogger.EventState", "State")
                        .WithMany()
                        .HasForeignKey("_stateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });
#pragma warning restore 612, 618
        }
    }
}
