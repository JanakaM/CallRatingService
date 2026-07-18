using CallRatingService.Application;
using CallRatingService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Infrastructure
{
    public class CallDetailRepository : ICallDetailRepository
    {
        private readonly ICallRatingServiceDbContext _dbContext;

        public CallDetailRepository(ICallRatingServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveCallDetail(List<CallDetail> callDetail)
        {
             _dbContext.CallDetails.AddRange(callDetail) ;

            var rowaffected = await _dbContext.SaveChangesAsync();

            return rowaffected;
        }
    }
}