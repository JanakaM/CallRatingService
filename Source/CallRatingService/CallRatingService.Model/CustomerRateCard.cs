using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CallRatingService.Model
{
    public class CustomerRateCard
    {
        [Key]
        public int RateCardId { get; set; }
        public int CustomerId { get; set; }

        public List<CustomerRate> Rates { get; set; } = new List<CustomerRate>();
    }
}
