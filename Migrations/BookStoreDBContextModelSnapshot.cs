﻿// <auto-generated />
using System;
using BookStoreP4.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStoreP4.Migrations
{
    [DbContext(typeof(BookStoreDBContext))]
    partial class BookStoreDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookStoreP4.DTOs.AuthorDTO", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorID"), 1L, 1);

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookDTOISBN")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AuthorID");

                    b.HasIndex("BookDTOISBN");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.BookDTO", b =>
                {
                    b.Property<string>("ISBN")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("VAT")
                        .HasColumnType("real");

                    b.HasKey("ISBN");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.CustomerDTO", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"), 1L, 1);

                    b.Property<string>("CustomerCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPESEL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerStreet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.EmployeeDTO", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"), 1L, 1);

                    b.Property<string>("EmployeeCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeePESEL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeStreet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.OrderDTO", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<int?>("OrderCustomerIDCustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderEmployeeIDEmployeeID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("OrderCustomerIDCustomerID");

                    b.HasIndex("OrderEmployeeIDEmployeeID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.OrderItemDTO", b =>
                {
                    b.Property<int>("OrderItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemID"), 1L, 1);

                    b.Property<float>("BookBruttoValue")
                        .HasColumnType("real");

                    b.Property<float>("BookNettoValue")
                        .HasColumnType("real");

                    b.Property<float>("BookPrice")
                        .HasColumnType("real");

                    b.Property<float?>("BookVAT")
                        .HasColumnType("real");

                    b.Property<int?>("OrderDTOOrderID")
                        .HasColumnType("int");

                    b.Property<string>("OrderItemBookISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemID");

                    b.HasIndex("OrderDTOOrderID");

                    b.HasIndex("OrderItemBookISBN");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.AuthorDTO", b =>
                {
                    b.HasOne("BookStoreP4.DTOs.BookDTO", null)
                        .WithMany("Authors")
                        .HasForeignKey("BookDTOISBN");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.OrderDTO", b =>
                {
                    b.HasOne("BookStoreP4.DTOs.CustomerDTO", "OrderCustomerID")
                        .WithMany()
                        .HasForeignKey("OrderCustomerIDCustomerID");

                    b.HasOne("BookStoreP4.DTOs.EmployeeDTO", "OrderEmployeeID")
                        .WithMany()
                        .HasForeignKey("OrderEmployeeIDEmployeeID");

                    b.Navigation("OrderCustomerID");

                    b.Navigation("OrderEmployeeID");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.OrderItemDTO", b =>
                {
                    b.HasOne("BookStoreP4.DTOs.OrderDTO", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderDTOOrderID");

                    b.HasOne("BookStoreP4.DTOs.BookDTO", "OrderItemBook")
                        .WithMany()
                        .HasForeignKey("OrderItemBookISBN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderItemBook");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.BookDTO", b =>
                {
                    b.Navigation("Authors");
                });

            modelBuilder.Entity("BookStoreP4.DTOs.OrderDTO", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
