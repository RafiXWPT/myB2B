using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using MyB2B.Domain.Companies;
using MyB2B.Domain.Identity;
using MyB2B.Domain.Invoices;

namespace MyB2B.Domain.EntityFramework
{
    public class MyB2BContext : DbContext
    {
        /* IDENTITY */
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationRight> Rights { get; set; }

        /* COMPANY */
        public DbSet<Company> Companies {get; set;}
        public DbSet<Address> Addresses {get; set;}
        public DbSet<CompanyProduct> CompanyProducts {get;set;}
        public DbSet<CompanyClient> CompanyClients {get;set;}

        /* INVOICE */
        public DbSet<Invoice> Invoices {get;set;}
        public DbSet<InvoicePosition> InvoicePositions {get;set;}

        private readonly IApplicationPrincipal _applicationPrincipal;

        public MyB2BContext(DbContextOptions<MyB2BContext> options, Func<IApplicationPrincipal> applicationPrincipalFunc) : base(options)
        {
            _applicationPrincipal = applicationPrincipalFunc();
        }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        private void Audit()
        {
            if (_applicationPrincipal == null)
                return;

            var time = DateTime.Now;
            var entitiesToAudit = GetAdderOrModifiedEntities();
            foreach (var entity in entitiesToAudit)
            {
                if (entity is IAuditableEntity auditableEntity)
                {
                    auditableEntity.AuditModified(time, _applicationPrincipal.UserId);
                }

                if (entity is IImmutableEntity immutableEntity)
                {
                    immutableEntity.AuditCreated(time, _applicationPrincipal.UserId);
                }
            }
        }

        private List<object> GetAdderOrModifiedEntities()
        {
            return this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Where(e => e.Entity is IAuditableEntity || e.Entity is IImmutableEntity)
                .Select(e => e.Entity)
                .ToList();
        }
    }

    public class MyB2BContextDesignTime : IDesignTimeDbContextFactory<MyB2BContext>
    {
        public MyB2BContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyB2BContext>().UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=MyB2B;Integrated Security=True;MultipleActiveResultSets=True;");

            return new MyB2BContext(optionsBuilder.Options, () => null);
        }
    }
}