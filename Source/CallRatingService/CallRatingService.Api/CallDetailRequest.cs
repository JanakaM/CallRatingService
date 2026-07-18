using System.ComponentModel.DataAnnotations;

namespace CallRatingService.Api
{
    public class CallDetailRequest
    {
        public int CustomerNumber { get; set; }

        [Required]
        public DateTime CallDate { get; set; }

        public string DestinationNumber { get; set; }

        public int DurationSeconds { get; set; }
    }
}
