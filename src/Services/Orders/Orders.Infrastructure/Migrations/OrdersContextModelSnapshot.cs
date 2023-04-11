﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orders.Infrastructure.DbContexts;

#nullable disable

namespace Orders.Infrastructure.Migrations
{
    [DbContext(typeof(OrdersContext))]
    partial class OrdersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Buyer")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("buyer");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDraft")
                        .HasColumnType("bit")
                        .HasColumnName("is_draft");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("modified_by");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("modified_date")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("number");

                    b.Property<int>("Weight")
                        .HasColumnType("int")
                        .HasColumnName("weight");

                    b.Property<int>("_paymentMethodId")
                        .HasColumnType("int")
                        .HasColumnName("payment_method_id");

                    b.Property<int>("_shippingMethodId")
                        .HasColumnType("int")
                        .HasColumnName("shipping_method_id");

                    b.Property<int>("_statusId")
                        .HasColumnType("int")
                        .HasColumnName("status_id");

                    b.Property<int>("_weightUnitId")
                        .HasColumnType("int")
                        .HasColumnName("weight_unit_id");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.HasIndex("_paymentMethodId");

                    b.HasIndex("_shippingMethodId");

                    b.HasIndex("_statusId");

                    b.HasIndex("_weightUnitId");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("discount");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("modified_by");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("modified_date")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("product_name");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("unit_price");

                    b.Property<byte>("Units")
                        .HasColumnType("tinyint")
                        .HasColumnName("units");

                    b.Property<long>("_orderId")
                        .HasColumnType("bigint")
                        .HasColumnName("order_id");

                    b.HasKey("Id");

                    b.HasIndex("_orderId");

                    b.ToTable("order_items", (string)null);
                });

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.OrderStatus", b =>
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

                    b.ToTable("order_statuses", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Submitted"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Awaiting validation"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Stock confirmed"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Paid"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Shipped"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.PaymentMethod", b =>
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

                    b.ToTable("payment_methods", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cash on delivery"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Debit card"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Credit card"
                        });
                });

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.ShippingMethod", b =>
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

                    b.ToTable("shipping_methods", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fedex"
                        },
                        new
                        {
                            Id = 2,
                            Name = "UPS"
                        },
                        new
                        {
                            Id = 3,
                            Name = "USPS"
                        });
                });

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.WeightUnit", b =>
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

                    b.ToTable("weight_units", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "g"
                        },
                        new
                        {
                            Id = 2,
                            Name = "kg"
                        },
                        new
                        {
                            Id = 3,
                            Name = "t"
                        });
                });

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.HasOne("Orders.Domain.AggregatesModel.OrderAggregate.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("_paymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Orders.Domain.AggregatesModel.OrderAggregate.ShippingMethod", "ShippingMethod")
                        .WithMany()
                        .HasForeignKey("_shippingMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Orders.Domain.AggregatesModel.OrderAggregate.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("_statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Orders.Domain.AggregatesModel.OrderAggregate.WeightUnit", "WeightUnit")
                        .WithMany()
                        .HasForeignKey("_weightUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Common.Domain.Models.Address", "Address", b1 =>
                        {
                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)")
                                .HasColumnName("country");

                            b1.Property<string>("State")
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)")
                                .HasColumnName("state");

                            b1.Property<string>("Street")
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)")
                                .HasColumnName("street");

                            b1.Property<string>("ZipCode")
                                .HasMaxLength(250)
                                .HasColumnType("nvarchar(250)")
                                .HasColumnName("zip_code");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("PaymentMethod");

                    b.Navigation("ShippingMethod");

                    b.Navigation("Status");

                    b.Navigation("WeightUnit");
                });

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("Orders.Domain.AggregatesModel.OrderAggregate.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("_orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Orders.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
