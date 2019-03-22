using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyB2B.Domain.EntityFramework.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerAddressId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerCompany",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerName",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerNip",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DealerAddressId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerCompany",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerName",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerNip",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Invoices",
                type: "decimal(12,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PaymentBankAccount",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentBankName",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Invoices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentToDate",
                table: "Invoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "Template",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortCode",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BuyerAddressId",
                table: "Invoices",
                column: "BuyerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DealerAddressId",
                table: "Invoices",
                column: "DealerAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Addresses_BuyerAddressId",
                table: "Invoices",
                column: "BuyerAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Addresses_DealerAddressId",
                table: "Invoices",
                column: "DealerAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Addresses_BuyerAddressId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Addresses_DealerAddressId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BuyerAddressId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_DealerAddressId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BuyerAddressId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BuyerCompany",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BuyerName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BuyerNip",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DealerAddressId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DealerCompany",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DealerName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DealerNip",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaymentBankAccount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaymentBankName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaymentToDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Template",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ShortCode",
                table: "Companies");
        }
    }
}
