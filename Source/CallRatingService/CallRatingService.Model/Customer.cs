using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Model
{
    public class Customer
    {
        public int CustomerId {  get; set; }

        public CustomerRateCard CustomerRateCard { get; set; }
    }
}
