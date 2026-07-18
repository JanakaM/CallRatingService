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

        public CallDetailCommandHandler(
            ICallDetailRepository detailRepository,
            ICallRateService callRateService)
        {
            _detailRepository = detailRepository;
            _callRateService = callRateService;
        }

        public async Task<List<RatedOutputResponse>> Handle(CallDetailCommand request, CancellationToken cancellationToken)
        {
            var callDetail = request.CallDetails.Select(c => new CallDetail()
            {
                CustomerId = c.CustomerNumber,
                CallDate = c.CallDate,
                DestinationNumber = c.DestinationNumber,
                DurationSeconds = c.DurationSeconds
            }).ToList();

            // Clculate call cost
            var result = await _callRateService.CalculateCallRate(callDetail);

            // save the CDR 
            await _detailRepository.SaveCallDetail(callDetail);

            return result;
        }
    }
}
