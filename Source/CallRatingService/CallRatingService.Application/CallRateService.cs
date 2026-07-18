using CallRatingService.Model;
using CallRatingService.Model.Enum;

namespace CallRatingService.Application
{
    public class CallRateService : ICallRateService
    {
        private readonly IRateCardRepository _rateCardRepository;

        public CallRateService(IRateCardRepository rateCardRepository)
        {
            _rateCardRepository = rateCardRepository;
        }

        public async Task<List<RatedOutputResponse>> CalculateCallRate(List<CallDetail> callDetails)
        {
            var response = new List<RatedOutputResponse>();

            foreach (var callDetail in callDetails) {

                var type = CallType.GetCallTypeByNumber(callDetail.DestinationNumber);

                var rateCard = await _rateCardRepository.GetRateCardAsync(callDetail.CustomerId);

                var rate = rateCard.Rates.Find(r => r.CallType == type.Type);
                var minute = CalculateBillableMinutes(callDetail.DurationSeconds);

                response.Add(new RatedOutputResponse()
                {
                    CustomerId = callDetail.CustomerId,
                    DestinationNumber = callDetail.DestinationNumber,
                    CallType = type.Type,
                    DurationSeconds = callDetail.DurationSeconds,
                    BillableMinutes = minute,
                    RateApplied = rate.CostperMinute,
                    Cost = rate.CostperMinute * minute
                });
            }

            return response;
        }

        private int CalculateBillableMinutes(int durationSeconds)
        {
            if (durationSeconds <= 0) return 0;

            return (int)Math.Ceiling(durationSeconds / 60.0);
        }
    }
}
