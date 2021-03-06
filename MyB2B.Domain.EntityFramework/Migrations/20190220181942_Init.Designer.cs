﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyB2B.Domain.EntityFramework.Migrations
{
    [DbContext(typeof(MyB2BContext))]
    [Migration("20190220181942_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("myB2B.Domain.Address", b =>
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

            modelBuilder.Entity("myB2B.Domain.Company.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("ModifiedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Nip");

                    b.Property<string>("Regon");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("myB2B.Domain.Company.CompanyClient", b =>
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

                    b.Property<string>("CreatedBy");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("ModifiedBy");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyClients");
                });

            modelBuilder.Entity("myB2B.Domain.Company.CompanyProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("ModifiedBy");

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<decimal>("NetPrice")
                        .HasColumnType("decimal(12,5)");

                    b.Property<double>("VatRate");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyProducts");
                });

            modelBuilder.Entity("myB2B.Domain.Identity.ApplicationRight", b =>
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

            modelBuilder.Entity("myB2B.Domain.Identity.ApplicationRole", b =>
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

            modelBuilder.Entity("myB2B.Domain.Identity.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int>("Status");

                    b.Property<int?>("UserCompanyId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("UserCompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("myB2B.Domain.Invoice.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("Generated");

                    b.Property<double>("InvoiceDiscount");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("ModifiedBy");

                    b.Property<string>("Number")
                        .HasMaxLength(255);

                    b.Property<int>("Status");

                    b.Property<decimal>("TotalGrossAmount")
                        .HasColumnType("decimal(12,5)");

                    b.Property<decimal>("TotalNetAmount")
                        .HasColumnType("decimal(12,5)");

                    b.Property<decimal>("TotalTaxAmount")
                        .HasColumnType("decimal(12,5)");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("myB2B.Domain.Invoice.InvoicePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<int?>("InvoiceId");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("ModifiedBy");

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

            modelBuilder.Entity("myB2B.Domain.Company.Company", b =>
                {
                    b.HasOne("myB2B.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("myB2B.Domain.Company.CompanyClient", b =>
                {
                    b.HasOne("myB2B.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("myB2B.Domain.Company.Company")
                        .WithMany("Clients")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("myB2B.Domain.Company.CompanyProduct", b =>
                {
                    b.HasOne("myB2B.Domain.Company.Company")
                        .WithMany("Products")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("myB2B.Domain.Identity.ApplicationRight", b =>
                {
                    b.HasOne("myB2B.Domain.Identity.ApplicationRole")
                        .WithMany("Rights")
                        .HasForeignKey("ApplicationRoleId");
                });

            modelBuilder.Entity("myB2B.Domain.Identity.ApplicationRole", b =>
                {
                    b.HasOne("myB2B.Domain.Identity.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("myB2B.Domain.Identity.ApplicationUser", b =>
                {
                    b.HasOne("myB2B.Domain.Company.Company", "UserCompany")
                        .WithMany()
                        .HasForeignKey("UserCompanyId");
                });

            modelBuilder.Entity("myB2B.Domain.Invoice.InvoicePosition", b =>
                {
                    b.HasOne("myB2B.Domain.Invoice.Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("myB2B.Domain.Company.CompanyProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
