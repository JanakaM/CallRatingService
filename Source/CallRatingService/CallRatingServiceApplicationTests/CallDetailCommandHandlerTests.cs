using CallRatingService.Application;
using CallRatingService.Application.Command;
using CallRatingService.Model;
using CallRatingService.Model.Enum;
using Moq;

namespace CallRatingServiceApplicationTests
{
    public class CallDetailCommandHandlerTests
    {
        private readonly Mock<ICallDetailRepository> _detailRepository;
        private readonly Mock<ICallRateService> _callRateService;
        private readonly Mock<ICustomerRepository> _customerRepository;

        public CallDetailCommandHandlerTests()
        {
            _detailRepository = new Mock<ICallDetailRepository>();
            _customerRepository = new Mock<ICustomerRepository>();
            _callRateService = new Mock<ICallRateService>();
        }

        [Fact]
        public async Task GivenCallDeatilCommand_Handle()
        {
            // Arrange
            var destinartionNumber = "+4407960507654";

            _customerRepository.Setup(x => x.GetCustomerWithRatesAsync(It.IsAny<int>()))
                .ReturnsAsync(new Customer()
                {
                    CustomerRateCard = new CustomerRateCard()
                    {
                        Rates = new List<CustomerRate>() { new CustomerRate() { CallType = "UK", CostPerMinute = 0.05m } }
                    }
                });

            _callRateService.Setup(x => x.CalculateCallRate(It.IsAny<CallDetail>())).ReturnsAsync(new RatedOutputResponse()
            {
                CallType = CallType.UK.Type,
            });

            var handler = new CallDetailCommandHandler(
                _detailRepository.Object,
                _callRateService.Object,
                _customerRepository.Object);

            var commandRequest = new CallDetailCommand()
            {
                CallDetails = new List<CallData>() { new CallData { CustomerNumber = 1001, CallDate = DateTime.Now.ToString(), DestinationNumber = destinartionNumber, DurationSeconds = 120 } }
            };

            //Act
            var result = await handler.Handle(commandRequest, new CancellationToken());

            //Assert

            Assert.Equal(CallType.UK.Type, result[0].CallType);

            _customerRepository.Verify(x => x.GetCustomerWithRatesAsync(
                It.Is<int>(id => id == 1001)
            ), Times.Once);

            _callRateService.Verify(x => x.CalculateCallRate(
                It.Is<CallDetail>(call => call.DestinationNumber == destinartionNumber && call.DurationSeconds == 120)
            ), Times.Once);

            _detailRepository.Verify(x => x.SaveCallDetail(It.Is<List<CallDetail>>(list =>
                list.Count == 1 &&
                list[0].DestinationNumber == destinartionNumber &&
                list[0].CustomerId == 1001)), Times.Once);
        }
    }
}
