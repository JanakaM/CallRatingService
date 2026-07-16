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

       public DbSet<CallDetail> CallDetails { get; set; }
    }
}
