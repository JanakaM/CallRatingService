using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Model
{
    public class CutomerRateCard
    {
        public int CustomerId { get; set; }

        public List<CustomerRate> Rates { get; set; } = new List<CustomerRate>();
    }
}
