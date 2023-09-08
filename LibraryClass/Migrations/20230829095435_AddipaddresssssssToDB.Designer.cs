﻿// <auto-generated />
using System;
using LibraryClass.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryClass.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20230829095435_AddipaddresssssssToDB")]
    partial class AddipaddresssssssToDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryClass.Models.Customer", b =>
                {
                    b.Property<string>("Cust_Email")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Cust_FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Cust_LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<long>("Cust_Phone")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Cust_Email");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("LibraryClass.Models.Item", b =>
                {
                    b.Property<string>("ItemName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("IQuantity")
                        .HasColumnType("int");

                    b.Property<int>("IRate")
                        .HasColumnType("int");

                    b.HasKey("ItemName");

                    b.ToTable("items");
                });

            modelBuilder.Entity("LibraryClass.Models.PlaceOrder", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<string>("Cust_Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("IQuantity")
                        .HasColumnType("int");

                    b.Property<int>("IRate")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ipAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderID");

                    b.ToTable("placeOrders");
                });

            modelBuilder.Entity("LibraryClass.Models.Register", b =>
                {
                    b.Property<int>("RegisterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegisterID"));

                    b.Property<string>("Confirm_Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastPassword1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastPassword2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RegisterID");

                    b.ToTable("registers");
                });
#pragma warning restore 612, 618
        }
    }
}
