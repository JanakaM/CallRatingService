using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CallRatingService.Model
{
    public class CustomerRate
    {
        public int RateId { get; set; }
        public int CustomerRateCustomerId { get; set; }
        public string CallType { get; set; }
        
        public decimal CostPerMinute { get; set; }

    }
}
