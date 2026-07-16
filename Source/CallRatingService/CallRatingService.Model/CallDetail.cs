namespace CallRatingService.Model
{
    public class CallDetail
    {
        public int CallId { get; set; }

        public int CustomerNumber { get; set; }

        public DateTime CallDate { get; set; }

        public string DestinationNumber { get; set; }  

        public int DurationSeconds { get; set; }
    }
}
