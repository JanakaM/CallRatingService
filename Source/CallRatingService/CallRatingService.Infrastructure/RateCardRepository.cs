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

            if (rateCard == null)
            {
                return new CustomerRateCardResponse()
                {
                    CustomerID = cutomerId,
                    Rates = new List<CutomerRates>()
                };
            }

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

            var existingRateCard = await _dbContext
                .CustomerRateCards
                .Include(r => r.Rates)
                .FirstOrDefaultAsync(c  => c.CustomerId == customerRateCard.CustomerId);

            if (existingRateCard == null)
            {
                _dbContext.CustomerRateCards.Add(customerRateCard);
                await _dbContext.SaveChangesAsync();
                return customerRateCard.RateCardId;
            }

            // Delete existing rates and add requested rates
            existingRateCard.Rates.Clear();

            foreach (var newRate in customerRateCard.Rates)
            {
                existingRateCard.Rates.Add(newRate);
            }

            await _dbContext.SaveChangesAsync();

            return customerRateCard.RateCardId;
        }
    }
}
