using CallRatingService.Application.Exceptions;
using CallRatingService.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Application.Command
{
    public class CallDetailCommandHandler : IRequestHandler<CallDetailCommand, List<RatedOutputResponse>>
    {
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
            var callDetail = request.CallDetails.Select(c => new CallDetail()
            {
                CallDetailCustomerId = c.CustomerNumber,
                CallDate = c.CallDate,
                DestinationNumber = c.DestinationNumber,
                DurationSeconds = c.DurationSeconds
            }).ToList();

            await Validate(callDetail);

            // Clculate call cost
            var result = await _callRateService.CalculateCallRate(callDetail);

            // save the CDR 
            await _detailRepository.SaveCallDetail(callDetail);

            return result;
        }

        private async Task Validate(List<CallDetail> callDetail)
        {
            foreach (var item in callDetail)
            {
                var customer = await _customerRepository.GetCustomerAsync(item.CallDetailCustomerId);

                if (customer == null)
                {
                    throw new NotFoundException($"Customer with CustomerId {item.CallDetailCustomerId} does not exist.");
                }
            }
        }
    }
}
