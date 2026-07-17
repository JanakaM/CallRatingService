using CallRatingService.Application;
using CallRatingService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Infrastructure
{
    public class RateCardRepository : IRateCardRepository
    {
        private readonly ICallRatingServiceDbContext _dbContext;

        public RateCardRepository(ICallRatingServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerRateCardResponse> GetRateCardAsync(int cutomerId)
        {
            var rateCard = await _dbContext.CustomerRateCards
                .Include(r => r.Rates)
                .FirstOrDefaultAsync(c => c.CustomerId == cutomerId);

            var response = new CustomerRateCardResponse()
            {
                CustomerID = cutomerId,
                Rates = rateCard.Rates.Select(r => new CutomerRates
                {
                    CallType = r.CallType,
                    CostperMinute = r.CostPerMinute
                }).ToList()
            };

            return response;
        }

        public async Task<int> UpsertRateCardAsync(CustomerRateCard customerRateCard)
        {
            // get existing RateCard for the Customer

            var rateCards = await _dbContext
                .CustomerRateCards
                .Include(r => r.Rates)
                .FirstOrDefaultAsync(c  => c.CustomerId == customerRateCard.CustomerId);

            if (rateCards == null)
            {
                _dbContext.CustomerRateCards.Add(customerRateCard);
                await _dbContext.SaveChangesAsync();
                return customerRateCard.RateCardId;
            }

            // ToDo Update or delete existing rates

            return customerRateCard.RateCardId;
        }
    }
}
