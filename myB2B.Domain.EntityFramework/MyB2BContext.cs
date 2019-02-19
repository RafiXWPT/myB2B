using Microsoft.EntityFrameworkCore;

namespace myB2B.Domain.EntityFramework
{
    public class MyB2BContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Company> Companies {get; set;}
        public DbSet<Address> Addresses {get; set;}
        public DbSet<CompanyProduct> CompanyProducts {get;set;}
        public DbSet<CompanyClient> CompanyClients {get;set;}
        public DbSet<Invoice> Invoices {get;set;}
        public DbSet<InvoicePosition> InvoicePositions {get;set;}

        public MyB2BContext(DbContextOptions<MyB2BContext> options) : base(options) { }
    }
}