﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RetailSystem.Infrastructure;

#nullable disable

namespace RetailSystem.Infrastructure.Migrations
{
    [DbContext(typeof(RetailDbContext))]
    partial class RetailDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RetailSystem.Core.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.ItemLedger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ReceiptItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptItemId");

                    b.ToTable("ItemLedgers");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.PurchaseOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PurchaseOrderStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderStatusId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.PurchaseOrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("PurchaseOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrderItems");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.PurchaseOrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrderStatuses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d0894321-8e96-4e4c-9951-9b43ddf78cc7"),
                            DisplayName = "New",
                            MachineName = "new"
                        },
                        new
                        {
                            Id = new Guid("c2fe6e42-629c-410f-b117-062fa59d0885"),
                            DisplayName = "Received",
                            MachineName = "received"
                        });
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.Receipt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PurchaseOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.ReceiptItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PurchaseOrderItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("ReceiptId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderItemId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptItems");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.SupplierStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SupplierStatuses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("32bd22b9-4178-4a68-b2bc-f24b130985a2"),
                            DisplayName = "Active",
                            MachineName = "active"
                        },
                        new
                        {
                            Id = new Guid("f89cce6a-c6eb-47fa-b567-fa8c3975fd83"),
                            DisplayName = "Inactive",
                            MachineName = "inactive"
                        });
                });

            modelBuilder.Entity("RetailSystem.Core.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("Email", "Phone")
                        .IsUnique();

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.ItemLedger", b =>
                {
                    b.HasOne("RetailSystem.Core.Entities.ReceiptItem", "ReceiptItem")
                        .WithMany()
                        .HasForeignKey("ReceiptItemId");

                    b.Navigation("ReceiptItem");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.PurchaseOrder", b =>
                {
                    b.HasOne("RetailSystem.Core.Entities.PurchaseOrderStatus", "PurchaseOrderStatus")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("PurchaseOrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PurchaseOrderStatus");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.PurchaseOrderItem", b =>
                {
                    b.HasOne("RetailSystem.Core.Entities.Item", "Item")
                        .WithMany("PurchaseOrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RetailSystem.Core.Entities.PurchaseOrder", "PurchaseOrder")
                        .WithMany("PurchaseOrderItems")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.Receipt", b =>
                {
                    b.HasOne("RetailSystem.Core.Entities.PurchaseOrder", "PurchaseOrder")
                        .WithMany("Receipts")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.ReceiptItem", b =>
                {
                    b.HasOne("RetailSystem.Core.Entities.PurchaseOrderItem", "PurchaseOrderItem")
                        .WithMany("ReceiptItems")
                        .HasForeignKey("PurchaseOrderItemId");

                    b.HasOne("RetailSystem.Core.Entities.Receipt", "Receipt")
                        .WithMany("ReceiptItems")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PurchaseOrderItem");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("RetailSystem.Core.Supplier", b =>
                {
                    b.HasOne("RetailSystem.Core.Entities.SupplierStatus", "Status")
                        .WithMany("Suppliers")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.Item", b =>
                {
                    b.Navigation("PurchaseOrderItems");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.PurchaseOrder", b =>
                {
                    b.Navigation("PurchaseOrderItems");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.PurchaseOrderItem", b =>
                {
                    b.Navigation("ReceiptItems");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.PurchaseOrderStatus", b =>
                {
                    b.Navigation("PurchaseOrders");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.Receipt", b =>
                {
                    b.Navigation("ReceiptItems");
                });

            modelBuilder.Entity("RetailSystem.Core.Entities.SupplierStatus", b =>
                {
                    b.Navigation("Suppliers");
                });
#pragma warning restore 612, 618
        }
    }
}
