using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application
{
    public class CustomerRateCardResponse
    {
        public int CustomerID { get; set; }
        public List<CutomerRates> Rates {  get; set; }
    }

    public  class CutomerRates
    {
        public string CallType { get; set; }
        public decimal CostperMinute { get; set; }
    } 
}
