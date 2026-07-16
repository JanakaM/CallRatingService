using CallRatingService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Infrastructure
{
    public interface ICallRatingServiceDbContext
    {
        DbSet<CallDetail> CallDetails { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
