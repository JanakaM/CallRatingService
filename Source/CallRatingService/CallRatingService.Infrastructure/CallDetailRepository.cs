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

        public async Task<int> SaveCallDetail(CallDetail callDetail)
        {
            var id = _dbContext.CallDetails.Add(callDetail) ;

            await _dbContext.SaveChangesAsync();

            return callDetail.CallId;
        }
    }
}