﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StorageManagement.API.Data;

namespace StorageManagement.API.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20201202153747_testmig2")]
    partial class testmig2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("StorageManagement.API.Models.ContractorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("NIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contractors");
                });

            modelBuilder.Entity("StorageManagement.API.Models.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StorageManagement.API.Models.ShelfModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RackId")
                        .HasColumnType("int");

                    b.Property<string>("ShelfNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("RackId");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("StorageManagement.API.Models.StorageRackModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ContractorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsTaken")
                        .HasColumnType("bit");

                    b.Property<string>("RackNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("StorageRacks");
                });

            modelBuilder.Entity("StorageManagement.API.Models.WarehouseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("StorageManagement.API.Models.ShelfModel", b =>
                {
                    b.HasOne("StorageManagement.API.Models.ProductModel", "Product")
                        .WithOne("Shelf")
                        .HasForeignKey("StorageManagement.API.Models.ShelfModel", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StorageManagement.API.Models.StorageRackModel", "Rack")
                        .WithMany("Shelves")
                        .HasForeignKey("RackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Rack");
                });

            modelBuilder.Entity("StorageManagement.API.Models.StorageRackModel", b =>
                {
                    b.HasOne("StorageManagement.API.Models.ContractorModel", "Contractor")
                        .WithMany("Racks")
                        .HasForeignKey("ContractorId");

                    b.HasOne("StorageManagement.API.Models.WarehouseModel", "Warehouse")
                        .WithMany("StorageRacks")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contractor");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("StorageManagement.API.Models.ContractorModel", b =>
                {
                    b.Navigation("Racks");
                });

            modelBuilder.Entity("StorageManagement.API.Models.ProductModel", b =>
                {
                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("StorageManagement.API.Models.StorageRackModel", b =>
                {
                    b.Navigation("Shelves");
                });

            modelBuilder.Entity("StorageManagement.API.Models.WarehouseModel", b =>
                {
                    b.Navigation("StorageRacks");
                });
#pragma warning restore 612, 618
        }
    }
}
