using CallRatingService.Application;
using CallRatingService.Model;
using CallRatingService.Model.Enum;
using Moq;

namespace CallRatingServiceApplicationTests
{
    public class CallRateServiceTests
    {
        [Fact]
        public async Task GivenCallDataRecord_CalcuateRate_ProducetRatedOutput()
        {
            //arrage

            var customerId = 1001;
            var destinationNumber = "+4401234564587";

            var callData = new CallDetail() { CustomerId = customerId, DestinationNumber = destinationNumber, DurationSeconds = 125 };

            var ratecard = new CustomerRateCardResponse()
            {
                CustomerID = customerId,
                Rates = new List<CutomerRates>()
                {
                   new(){ CallType = CallType.UK.Type, CostperMinute = 0.05m },
                   new(){ CallType = CallType.USA.Type, CostperMinute = 0.10m }
                }
            };

            var repository = new Mock<IRateCardRepository>();
            repository.Setup(x => x.GetRateCardAsync(customerId)).ReturnsAsync(ratecard);

            var service = new CallRateService(repository.Object);

            // Act

            var result = await service.CalculateCallRate(callData);

            // Asset

            Assert.Equal(CallType.UK.Type, result.CallType);
            Assert.Equal(3, result.BillableMinutes);
            Assert.Equal(0.05m, result.RateApplied);
            Assert.Equal(0.15m, result.Cost);
        }
    }
}
