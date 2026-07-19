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
                e.HasKey(x => x.CustomerId);

                e.HasData(
                new Customer { CustomerId = 1001 },
                new Customer { CustomerId = 1002 },
                new Customer { CustomerId = 1003 },
                new Customer { CustomerId = 1004 },
                new Customer { CustomerId = 1005 },
                new Customer { CustomerId = 1006 },
                new Customer { CustomerId = 1007 },
                new Customer { CustomerId = 1008 },
                new Customer { CustomerId = 1009 },
                new Customer { CustomerId = 1010 });
            });

            modelBuilder.Entity<CallDetail>()
                .HasKey(x => x.CallId);

            modelBuilder.Entity<CustomerRate>()
                .HasKey(x => x.RateId);

            modelBuilder.Entity<CustomerRateCard>(e =>
            {
                e.HasKey(x => x.RateCardId);

                e.HasMany(c => c.Rates)
                .WithOne()
                .HasForeignKey(r => r.CustomerRateCustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }

       public DbSet<Customer> Customers { get; set; }
       public DbSet<CallDetail> CallDetails { get; set; }
       public DbSet<CustomerRateCard> CustomerRateCards { get; set; }
       public DbSet<CustomerRate> CustomerRates { get; set; }
    }
}
