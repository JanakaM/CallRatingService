namespace CallRatingService.Api
{
    public class RateRequest
    {
        public string CallType { get; set; }
        public decimal CostPerMinute { get; set; }
    }
}
