﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyB2B.Domain.EntityFramework;

namespace MyB2B.Domain.EntityFramework.Migrations
{
    [DbContext(typeof(MyB2BContext))]
    [Migration("20190322221036_Init3")]
    partial class Init3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyB2B.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasMaxLength(255);

                    b.Property<string>("Country")
                        .HasMaxLength(255);

                    b.Property<string>("Number")
                        .HasMaxLength(10);

                    b.Property<string>("Street")
                        .HasMaxLength(255);

                    b.Property<string>("ZipCode")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("MyB2B.Domain.Companies.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Nip");

                    b.Property<string>("Regon");

                    b.Property<string>("ShortCode");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("MyB2B.Domain.Companies.CompanyClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<int?>("CompanyId");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(255);

                    b.Property<string>("CompanyNip")
                        .HasMaxLength(40);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyClients");
                });

            modelBuilder.Entity("MyB2B.Domain.Companies.CompanyProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<decimal>("NetPrice")
                        .HasColumnType("decimal(12,5)");

                    b.Property<double>("VatRate");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyProducts");
                });

            modelBuilder.Entity("MyB2B.Domain.Identity.ApplicationRight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationRoleId");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Symbol");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationRoleId");

                    b.ToTable("Rights");
                });

            modelBuilder.Entity("MyB2B.Domain.Identity.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationUserId");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Symbol");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MyB2B.Domain.Identity.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<int>("UserCompanyId");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserCompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyB2B.Domain.Invoices.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuyerAddressId");

                    b.Property<string>("BuyerCompany");

                    b.Property<string>("BuyerName");

                    b.Property<string>("BuyerNip");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<int?>("DealerAddressId");

                    b.Property<string>("DealerCompany");

                    b.Property<string>("DealerName");

                    b.Property<string>("DealerNip");

                    b.Property<DateTime>("Generated");

                    b.Property<double>("InvoiceDiscount");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<string>("Number")
                        .HasMaxLength(255);

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("decimal(12,5)");

                    b.Property<string>("PaymentBankAccount");

                    b.Property<string>("PaymentBankName");

                    b.Property<int>("PaymentMethod");

                    b.Property<DateTime>("PaymentToDate");

                    b.Property<int>("Status");

                    b.Property<byte[]>("Template");

                    b.Property<decimal>("TotalGrossAmount")
                        .HasColumnType("decimal(12,5)");

                    b.Property<decimal>("TotalNetAmount")
                        .HasColumnType("decimal(12,5)");

                    b.Property<decimal>("TotalTaxAmount")
                        .HasColumnType("decimal(12,5)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerAddressId");

                    b.HasIndex("DealerAddressId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("MyB2B.Domain.Invoices.InvoicePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<int?>("InvoiceId");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<int>("ModifiedBy");

                    b.Property<double>("ProductDiscount");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("TotalGrossAmount")
                        .HasColumnType("decimal(12,5)");

                    b.Property<decimal>("TotalNetAmount")
                        .HasColumnType("decimal(12,5)");

                    b.Property<decimal>("TotalTaxAmount")
                        .HasColumnType("decimal(12,5)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("InvoicePositions");
                });

            modelBuilder.Entity("MyB2B.Domain.Companies.Company", b =>
                {
                    b.HasOne("MyB2B.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("MyB2B.Domain.Companies.CompanyClient", b =>
                {
                    b.HasOne("MyB2B.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("MyB2B.Domain.Companies.Company")
                        .WithMany("Clients")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("MyB2B.Domain.Companies.CompanyProduct", b =>
                {
                    b.HasOne("MyB2B.Domain.Companies.Company")
                        .WithMany("Products")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("MyB2B.Domain.Identity.ApplicationRight", b =>
                {
                    b.HasOne("MyB2B.Domain.Identity.ApplicationRole")
                        .WithMany("Rights")
                        .HasForeignKey("ApplicationRoleId");
                });

            modelBuilder.Entity("MyB2B.Domain.Identity.ApplicationRole", b =>
                {
                    b.HasOne("MyB2B.Domain.Identity.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("MyB2B.Domain.Identity.ApplicationUser", b =>
                {
                    b.HasOne("MyB2B.Domain.Companies.Company", "UserCompany")
                        .WithMany()
                        .HasForeignKey("UserCompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyB2B.Domain.Invoices.Invoice", b =>
                {
                    b.HasOne("MyB2B.Domain.Address", "BuyerAddress")
                        .WithMany()
                        .HasForeignKey("BuyerAddressId");

                    b.HasOne("MyB2B.Domain.Address", "DealerAddress")
                        .WithMany()
                        .HasForeignKey("DealerAddressId");
                });

            modelBuilder.Entity("MyB2B.Domain.Invoices.InvoicePosition", b =>
                {
                    b.HasOne("MyB2B.Domain.Invoices.Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("MyB2B.Domain.Companies.CompanyProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
