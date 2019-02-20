using myB2B.Domain.Company;
using myB2B.Domain.Identity;
using myB2B.Domain.Invoice;
using Microsoft.EntityFrameworkCore;

namespace myB2B.Domain.EntityFramework
{
    public class MyB2BContext : DbContext
    {
        /* IDENTITY */
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationRight> Rights { get; set; }

        /* COMPANY */
        public DbSet<Company.Company> Companies {get; set;}
        public DbSet<Address> Addresses {get; set;}
        public DbSet<CompanyProduct> CompanyProducts {get;set;}
        public DbSet<CompanyClient> CompanyClients {get;set;}

        /* INVOICE */
        public DbSet<Invoice.Invoice> Invoices {get;set;}
        public DbSet<InvoicePosition> InvoicePositions {get;set;}

        public MyB2BContext(DbContextOptions<MyB2BContext> options) : base(options) { }
    }
}