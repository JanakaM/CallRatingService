using System.ComponentModel.DataAnnotations;

namespace CallRatingService.Api
{
    public class CallDetailRequest
    {
        public int CustomerNumber { get; set; }

        public string CallDate { get; set; }

        public string DestinationNumber { get; set; }

        public int DurationSeconds { get; set; }
    }
}
