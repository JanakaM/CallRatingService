using CallRatingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application
{
    public class RatedOutputResponse
    {
        public int CustomerId { get; set; }

        public string DestinationNumber { get; set; }

        public int DurationSeconds { get; set; }

        public string CallType { get; set; } 

        public int BillableMinutes { get; set; }
        
        public decimal RateApplied { get; set; }

        public decimal Cost { get; set; }

        public bool Valid { get; set; } = true;

        public string ErrorMessage { get; set; }
    }
}
