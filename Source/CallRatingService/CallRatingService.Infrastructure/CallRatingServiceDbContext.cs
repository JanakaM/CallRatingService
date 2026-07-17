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

            modelBuilder.Entity<CustomerRateCard>()
                       .HasMany(c => c.Rates)
                       .WithOne()
                       .HasForeignKey(r => r.CustomerId)
                       .HasPrincipalKey(c => c.CustomerId)
                       .OnDelete(DeleteBehavior.Cascade);
        }

       public DbSet<CallDetail> CallDetails { get; set; }
       public DbSet<CustomerRateCard> CustomerRateCards { get; set; }
       public DbSet<CustomerRate> CustomerRates { get; set; }
    }
}
