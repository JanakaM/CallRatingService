using CallRatingService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application
{
    public interface IRateCardRepository
    {
        Task<int> UpsertRateCardAsync(CustomerRateCard cutomerRateCard);
        Task<CustomerRateCardResponse> GetRateCardAsync(int cutomerId);
    }
}
