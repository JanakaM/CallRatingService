using CallRatingService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application
{
    public interface ICallRateService
    {
        Task<RatedOutputResponse> CalculateCallRate(CallDetail callDetails);
    }
}
