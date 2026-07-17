using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CallRatingService.Model
{
    public class CustomerRate
    {
        [Key]
        public int RateId { get; set; }
        public int CustomerId { get; set; }
        public string CallType { get; set; }
        
        public decimal CostPerMinute { get; set; }

    }
}
