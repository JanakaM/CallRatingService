namespace CallRatingService.Api
{
    public class RateCardRequest
    {
        public int CustomerId { get; set; }

        public List<RateRequest> Rates { get; set; }
    }
}
