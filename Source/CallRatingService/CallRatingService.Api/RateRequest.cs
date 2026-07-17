namespace CallRatingService.Api
{
    public class RateRequest
    {
        public int CustomerId { get; set; }
        public string CallType { get; set; }

        public decimal CostPerMinute { get; set; }
    }
}
