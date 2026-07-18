using CallRatingService.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace CallRatingService.Infrastructure
{
    public class CallRatingServiceDbContext : DbContext, ICallRatingServiceDbContext
    {
        public CallRatingServiceDbContext(DbContextOptions<CallRatingServiceDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prepopulate Cutomer Data for Simply the Solution & and Demo purpose only
            // In real world application will have Migration script / Cutomer Add Endpoint 
            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.CustomerId);
                e.HasData(
                new Customer { Id = 1, CustomerId = 1001 },
                new Customer { Id = 2, CustomerId = 1002 },
                new Customer { Id = 3, CustomerId = 1003 },
                new Customer { Id = 4, CustomerId = 1004 },
                new Customer { Id = 5, CustomerId = 1005 },
                new Customer { Id = 6, CustomerId = 1006 },
                new Customer { Id = 7, CustomerId = 1007 },
                new Customer { Id = 8, CustomerId = 1008 },
                new Customer { Id = 9, CustomerId = 1009 },
                new Customer { Id = 10, CustomerId = 1010 });
            });

            modelBuilder.Entity<CallDetail>()
                .HasKey(x => x.CallId);

            modelBuilder.Entity<CustomerRate>()
                .HasKey(x => x.RateId);

            modelBuilder.Entity<CustomerRateCard>()
                       .HasMany(c => c.Rates)
                       .WithOne()
                       .HasForeignKey(r => r.CustomerRateCustomerId)
                       .OnDelete(DeleteBehavior.Cascade);
        }

       public DbSet<Customer> Customers { get; set; }
       public DbSet<CallDetail> CallDetails { get; set; }
       public DbSet<CustomerRateCard> CustomerRateCards { get; set; }
       public DbSet<CustomerRate> CustomerRates { get; set; }
    }
}
