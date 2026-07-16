using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Model
{
    public class CustomerRate
    {
        public int CustomerID { get; set; }
        public string CallType { get; set; }
        
        public double CostPerMinute { get; set; }

    }
}
