using System.ComponentModel.DataAnnotations;

namespace CallRatingService.Model
{
    public class CallDetail
    {
        [Key] 
        public int CallId { get; set; }

        public int CustomerId { get; set; }

        public DateTime CallDate { get; set; }

        public string DestinationNumber { get; set; }  

        public int DurationSeconds { get; set; }
    }
}
