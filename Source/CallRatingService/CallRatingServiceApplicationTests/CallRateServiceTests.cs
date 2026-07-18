using CallRatingService.Application;
using CallRatingService.Model;
using CallRatingService.Model.Enum;
using Moq;

namespace CallRatingServiceApplicationTests
{
    public class CallRateServiceTests
    {
        [Fact]
        public async Task GivenOneCallDataRecord_CalcuateRate_ProducetRatedOutput()
        {
            //arrage

            var customerId = 1001;
            var destinationNumber = "+4401234564587";

            var callData = new List<CallDetail>()
            {
                new CallDetail(){CustomerId = customerId, DestinationNumber = destinationNumber, DurationSeconds = 125 }
            };

            var ratecard = new CustomerRateCardResponse()
            {
                CustomerID = customerId,
                Rates = new List<CutomerRates>()
                {
                   new(){ CallType = CallType.UK.Type, CostperMinute = 0.05m },
                   new(){ CallType = CallType.USA.Type, CostperMinute = 0.15m },
                   new(){ CallType = CallType.Ireland.Type, CostperMinute = 0.20m },
                }
            };

            var repository = new Mock<IRateCardRepository>();
            repository.Setup(x => x.GetRateCardAsync(customerId)).ReturnsAsync(ratecard);

            var service = new CallRateService(repository.Object);

            // Act

            var result = await service.CalculateCallRate(callData);

            // Asset

            Assert.Equal(CallType.UK.Type, result[0].CallType);
            Assert.Equal(3, result[0].BillableMinutes);
            Assert.Equal(0.05m, result[0].RateApplied);
            Assert.Equal(0.15m, result[0].Cost);
        }
    }
}
