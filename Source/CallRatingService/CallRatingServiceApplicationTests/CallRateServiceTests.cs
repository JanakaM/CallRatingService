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
                new CallDetail(){CallDetailCustomerId = customerId, DestinationNumber = destinationNumber, DurationSeconds = 125 }
            };

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

            Assert.Equal(CallType.UK.Type, result[0].CallType);
            Assert.Equal(3, result[0].BillableMinutes);
            Assert.Equal(0.05m, result[0].RateApplied);
            Assert.Equal(0.15m, result[0].Cost);
        }

        [Fact]
        public async Task GivenTwoCallDataRecordsWithSameCustomer_CalcuateRate_ProducetRatedOutputWithTwoObjects()
        {
            //arrage

            var customerId = 1001;

            var destinationNumber1 = "+4401234564587";
            var destinationNumber2 = "+101234564123";

            var callData = new List<CallDetail>()
            {
                new CallDetail(){CallDetailCustomerId = customerId, DestinationNumber = destinationNumber1, DurationSeconds = 125 },
                new CallDetail(){CallDetailCustomerId = customerId, DestinationNumber = destinationNumber2, DurationSeconds = 125 }
            };

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
            Assert.Equal(customerId, result[0].CustomerId);
            Assert.Equal(CallType.UK.Type, result[0].CallType);
            Assert.Equal(3, result[0].BillableMinutes);
            Assert.Equal(0.05m, result[0].RateApplied);
            Assert.Equal(0.15m, result[0].Cost);

            Assert.Equal(customerId, result[1].CustomerId);
            Assert.Equal(CallType.USA.Type, result[1].CallType);
            Assert.Equal(3, result[1].BillableMinutes);
            Assert.Equal(0.10m, result[1].RateApplied);
            Assert.Equal(0.30m, result[1].Cost);
        }

        [Fact]
        public async Task GivenTwoCallDataRecordsWithDiffrentCustomer_CalcuateRate_ProducetRatedOutputWithTwoObjects()
        {
            //arrage

            var customerId_1 = 1001;
            var customerId_2 = 1002;

            var destinationNumber1 = "+4401234564587";
            var destinationNumber2 = "+101234564123";

            var callData = new List<CallDetail>()
            {
                new CallDetail(){CallDetailCustomerId = customerId_1, DestinationNumber = destinationNumber1, DurationSeconds = 125 },
                new CallDetail(){CallDetailCustomerId = customerId_2, DestinationNumber = destinationNumber2, DurationSeconds = 125 }
            };

            var ratecard_Cutomer_1 = new CustomerRateCardResponse()
            {
                CustomerID = customerId_1,
                Rates = new List<CutomerRates>()
                {
                   new(){ CallType = CallType.UK.Type, CostperMinute = 0.05m },
                   new(){ CallType = CallType.USA.Type, CostperMinute = 0.10m }
                }
            };

            var ratecard_Cutomer_2 = new CustomerRateCardResponse()
            {
                CustomerID = customerId_2,
                Rates = new List<CutomerRates>()
                {
                   new(){ CallType = CallType.UK.Type, CostperMinute = 0.05m },
                   new(){ CallType = CallType.USA.Type, CostperMinute = 0.20m }
                }
            };

            var repository = new Mock<IRateCardRepository>();
            repository.Setup(x => x.GetRateCardAsync(customerId_1)).ReturnsAsync(ratecard_Cutomer_1);
            repository.Setup(x => x.GetRateCardAsync(customerId_2)).ReturnsAsync(ratecard_Cutomer_2);

            var service = new CallRateService(repository.Object);

            // Act

            var result = await service.CalculateCallRate(callData);

            // Asset
            Assert.Equal(customerId_1, result[0].CustomerId);
            Assert.Equal(CallType.UK.Type, result[0].CallType);
            Assert.Equal(3, result[0].BillableMinutes);
            Assert.Equal(0.05m, result[0].RateApplied);
            Assert.Equal(0.15m, result[0].Cost);

            Assert.Equal(customerId_2, result[1].CustomerId);
            Assert.Equal(CallType.USA.Type, result[1].CallType);
            Assert.Equal(3, result[1].BillableMinutes);
            Assert.Equal(0.20m, result[1].RateApplied);
            Assert.Equal(0.60m, result[1].Cost);
        }
    }
}
