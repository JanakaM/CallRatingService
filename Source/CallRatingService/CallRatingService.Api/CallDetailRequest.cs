namespace CallRatingService.Api
{
    public class CallDetailRequest
    {
        public int CustomerNumber { get; set; }

        public DateTime CallDate { get; set; }

        public string DestinationNumber { get; set; }

        public int DurationSeconds { get; set; }
    }
}
