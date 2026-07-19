using CallRatingService.Model;
using CallRatingService.Model.Enum;
using MediatR;
using System.Text.RegularExpressions;

namespace CallRatingService.Application.Command
{
    public class CallDetailCommandHandler : IRequestHandler<CallDetailCommand, List<RatedOutputResponse>>
    {
        private static readonly Regex E164PhoneRegex = new Regex(@"^\+[1-9]\d{1,14}$", RegexOptions.Compiled);

        private readonly ICallDetailRepository _detailRepository;
        private readonly ICallRateService _callRateService;
        private readonly ICustomerRepository _customerRepository;

        public CallDetailCommandHandler(
            ICallDetailRepository detailRepository,
            ICallRateService callRateService,
            ICustomerRepository customerRepository)
        {
            _detailRepository = detailRepository;
            _callRateService = callRateService;
            _customerRepository = customerRepository;
        }

        public async Task<List<RatedOutputResponse>> Handle(CallDetailCommand request, CancellationToken cancellationToken)
        {
            var result = new List<RatedOutputResponse>();
            var validCallDetails = new List<CallDetail>();
;
            foreach (var item in request.CallDetails)
            {
                bool isValidCall = true;

                // Validate Destination Number
                isValidCall = ValidateDestinationNumber(result, item);
                if (!isValidCall)
                {
                    continue;
                }

                // Validate cutomerNumber + Rate available
                isValidCall = await ValidateCustomer(result, item);
                if (!isValidCall)
                {
                    continue;
                }

                // validation durationSeconds
                isValidCall = ValidateDurationSeconds(result, item);

                if (!isValidCall)
                {
                    continue;
                }

                // validation CallDate
                if (!DateTime.TryParse(item.CallDate, out DateTime parsedDate))
                {
                    result.Add(new RatedOutputResponse()
                    {
                        CustomerId = item.CustomerNumber,
                        DestinationNumber = item.DestinationNumber,
                        DurationSeconds = item.DurationSeconds,
                        Valid = false,
                        ErrorMessage = $"Call Date Invalid  : {item.CallDate}"
                    });
                    continue;
                }

                // Calculate call cost
                var callDetail = new CallDetail
                {
                    CallDate = parsedDate,
                    CallDetailCustomerId = item.CustomerNumber,
                    DestinationNumber = item.DestinationNumber,
                    DurationSeconds = item.DurationSeconds
                };

                var rateedResult = await _callRateService.CalculateCallRate(callDetail);
                result.Add(rateedResult);

                // Add CDR
                validCallDetails.Add(callDetail);
            }

            // save only valid CDRs 
            await _detailRepository.SaveCallDetail(validCallDetails);

            return result;
        }

        private static bool ValidateDurationSeconds(List<RatedOutputResponse> result, CallData item)
        {
            if (item.DurationSeconds <= 0)
            {
                result.Add(new RatedOutputResponse()
                {
                    CustomerId = item.CustomerNumber,
                    DestinationNumber = item.DestinationNumber,
                    DurationSeconds = item.DurationSeconds,
                    Valid = false,
                    ErrorMessage = $"Duration Seconds invalid: {item.DurationSeconds}"
                });
                return false;
            }

            return true;
        }

        private bool ValidateDestinationNumber(List<RatedOutputResponse> result, CallData item)
        {
            if (string.IsNullOrWhiteSpace(item.DestinationNumber))
            {
                result.Add(new RatedOutputResponse()
                {
                    CustomerId = item.CustomerNumber,
                    DestinationNumber = item.DestinationNumber,
                    DurationSeconds = item.DurationSeconds,
                    Valid = false,
                    ErrorMessage = $"Distination number invalid: {item.DestinationNumber}"
                });
                return false;
            }
            if (!E164PhoneRegex.IsMatch(item.DestinationNumber))
            {
                result.Add(new RatedOutputResponse()
                {
                    CustomerId = item.CustomerNumber,
                    DestinationNumber = item.DestinationNumber,
                    DurationSeconds = item.DurationSeconds,
                    Valid = false,
                    ErrorMessage = $"Distination number invalid: {item.DestinationNumber}"
                });
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateCustomer(List<RatedOutputResponse> result, CallData item)
        {
            if (item.CustomerNumber <= 0)
            {
                result.Add(new RatedOutputResponse()
                {
                    CustomerId = item.CustomerNumber,
                    DestinationNumber = item.DestinationNumber,
                    DurationSeconds = item.DurationSeconds,
                    Valid = false,
                    ErrorMessage = $"Customer not found for the id  : {item.CustomerNumber}"
                });
                return false;
            }

            var customerWithRates = await _customerRepository.GetCustomerWithRatesAsync(item.CustomerNumber);

            if (customerWithRates == null)
            {
                result.Add(new RatedOutputResponse()
                {
                    CustomerId = item.CustomerNumber,
                    DestinationNumber = item.DestinationNumber,
                    DurationSeconds = item.DurationSeconds,
                    Valid = false,
                    ErrorMessage = $"Customer Rates not found for the customer  : {item.CustomerNumber}"
                });
                return false;
            }

            // validate rate
            var type = CallType.GetCallTypeByNumber(item.DestinationNumber);

            var rate = customerWithRates.CustomerRateCard?.Rates.Find(r => r.CallType == type.Type);

            if (rate == null)
            {
                result.Add(new RatedOutputResponse()
                {
                    CustomerId = item.CustomerNumber,
                    DestinationNumber = item.DestinationNumber,
                    DurationSeconds = item.DurationSeconds,
                    Valid = false,
                    ErrorMessage = $"Rate not found  for: {item.DestinationNumber}"
                });
                return false;
            }

            return true;
        }
    }
}
